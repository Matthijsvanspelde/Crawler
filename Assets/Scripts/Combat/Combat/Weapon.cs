using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : Equipment
{
    [SerializeField] private WeaponStats baseStats;
    public Transform pointToHitFrom;
    public LayerMask CanHit;

    private float currentAttackSpeedTimer = 0f;

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, baseStats.AttackRange.GetValue());
    }

    public virtual void HandleWeapon()
    {

    }

    public virtual void HandleAttack()
    {
        if (CanAttack)
        {
            StartCoroutine(AttackSpeedTimer());
        }
    }

    public IEnumerator AttackSpeedTimer()
    {
        CanAttack = false;

        while (CanAttack == false)
        {
            currentAttackSpeedTimer += Time.deltaTime;

            if (currentAttackSpeedTimer >= baseStats.AttackSpeed.GetValue())
            {
                currentAttackSpeedTimer = 0f;
                CanAttack = true;
            }
            yield return null;
        }

        yield return null;
    }

    public bool CanAttack { get; private set; } = true;

    public WeaponStats Stats { get => baseStats;}
}
