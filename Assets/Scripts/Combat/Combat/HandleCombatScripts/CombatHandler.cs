using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    [SerializeField] private Transform pointToHitFrom;

    private WeaponStats weaponStats = null;
    private bool canAttack = true;
    private float currentAttackSpeedTimer = 0f;
    
    public void HandleRightAttack(WeaponStats stats)
    {
        weaponStats = stats;

        if (canAttack)
        {
            canAttack = false;
            StartCoroutine(AttackSpeedTimer());
            StartCoroutine(DoAttack());
        }
    }

    #region Coroutines

    IEnumerator AttackSpeedTimer()
    {
        while (canAttack == false)
        {
            currentAttackSpeedTimer += Time.deltaTime;

            if (currentAttackSpeedTimer >= weaponStats.AttackSpeed)
            {
                currentAttackSpeedTimer = 0f;
                canAttack = true;
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
            CheckForEnemyHit(hit);
        }
    }

    #endregion

    #region Logic
    private void TakeDamage()
    {
        //TODO: implement this for enemy and player

    }

    private void CheckForEnemyHit(RaycastHit hit)
    {
        if (hit.collider.tag == "Enemy")
        {
            EnemyHealth eHealth = hit.collider.GetComponent<EnemyHealth>();
            eHealth.TakeDamage(weaponStats.AttackDamage);
        }
    }

    private RaycastHit GetRayCastHit()
    {
        Ray ray = new Ray(pointToHitFrom.position, pointToHitFrom.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, weaponStats.AttackRange))
        {
            return hit;
        }

        return hit;
    }
    #endregion

    public bool CanAttack { get => canAttack; }
}
