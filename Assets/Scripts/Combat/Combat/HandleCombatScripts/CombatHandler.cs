using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    [SerializeField] private Transform pointToHitFrom;

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

            if (currentAttackSpeedTimer >= weaponStats.AttackSpeed)
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
        yield return new WaitForSeconds(weaponStats.StartupSpeed);
        RaycastHit hit = GetRayCastHit();

        if (hit.collider != null)
        {
            //we hit something
            HandleHit(hit);
        }
    }

    #endregion

    #region Logic

    private void HandleHit(RaycastHit hit)
    {
        StatHolder HitStatHolder =  hit.collider.GetComponent<StatHolder>();

        //Make sure we did not hit something NoneHitable
        if (HitStatHolder != null)
        {
            HitStatHolder.TakeDamage(weaponStats.RandomDamage());
        }
    }

    private RaycastHit GetRayCastHit()
    {
        Ray ray = new Ray(pointToHitFrom.position, pointToHitFrom.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, weaponStats.AttackRange))
        {
            Debug.DrawLine(pointToHitFrom.position, hit.point, Color.red,2f);

            return hit;
        }

        return hit;
    }
    #endregion

    public bool CanAttack { get; private set; } = true;
}
