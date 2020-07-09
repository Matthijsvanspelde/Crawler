using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public override void HandleAttack()
    {
        if (CanAttack)
        {
            base.HandleAttack();
            StartCoroutine(DoAttack());
        }
    }

    public IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(Stats.StartupSpeed.GetValue());
        RaycastHit hit = GetRayCastHit();
        if (hit.collider != null && !hit.collider.isTrigger)
        {
            //we hit something
            Debug.DrawLine(pointToHitFrom.position, hit.point, Color.red, 2f);
            HandleHit(hit);
        }

        HandleWeapon();
    }

    private void HandleHit(RaycastHit hit)
    {
        StatLine HitStatHolder = hit.collider.GetComponent<StatLine>();

        //Make sure we did not hit something NoneHitable
        if (HitStatHolder != null)
        {
            HitStatHolder.TakeDamage(Mathf.RoundToInt(Stats.RandomDamage()));
        }
    }

    private RaycastHit GetRayCastHit()
    {
        Ray ray = new Ray(pointToHitFrom.position, pointToHitFrom.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction, Color.blue, 10f);
        if (Physics.Raycast(ray, out hit, Stats.AttackRange.GetValue(), CanHit))
        {
            return hit;
        }
 
        return hit;
    }
}