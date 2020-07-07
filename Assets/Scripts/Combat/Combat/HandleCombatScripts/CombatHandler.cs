using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    [SerializeField] private Transform pointToHitFrom;
    [SerializeField] private LayerMask CanHit;

    private WeaponStats weaponStats = null;
    private float currentAttackSpeedTimer = 0f;
    
    public void HandleAttack(WeaponStats stats)
    {
        weaponStats = stats;

        if (CanAttack)
        {
            CanAttack = false;
            StartCoroutine(AttackSpeedTimer());
            StartCoroutine(DoAttack());
        }
    }

    #region Coroutines

    IEnumerator AttackSpeedTimer()
    {
        while (CanAttack == false)
        {
            currentAttackSpeedTimer += Time.deltaTime;

            if (currentAttackSpeedTimer >= weaponStats.AttackSpeed.GetValue())
            {
                currentAttackSpeedTimer = 0f;
                CanAttack = true;
            }
            yield return null;
        }

        yield return null;
    }

    IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(weaponStats.StartupSpeed.GetValue());
        RaycastHit hit = GetRayCastHit();

        if (hit.collider != null && !hit.collider.isTrigger)
        {
            //we hit something
            Debug.DrawLine(pointToHitFrom.position, hit.point, Color.red, 2f);
            HandleHit(hit);
        }
        
        weaponStats.HandleWeapon();
    }

    #endregion

    #region Logic

    private void HandleHit(RaycastHit hit)
    {
        StatLine HitStatHolder =  hit.collider.GetComponent<StatLine>();

        //Make sure we did not hit something NoneHitable
        if (HitStatHolder != null)
        {
            HitStatHolder.TakeDamage(Mathf.RoundToInt(weaponStats.RandomDamage()));
        }
    }

    private RaycastHit GetRayCastHit()
    {
        Ray ray = new Ray(pointToHitFrom.position, pointToHitFrom.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, weaponStats.AttackRange.GetValue(), CanHit))
        {
            return hit;
        }

        return hit;
    }
    #endregion

    public bool CanAttack { get; private set; } = true;
}
