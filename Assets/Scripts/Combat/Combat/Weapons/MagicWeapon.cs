using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWeapon : Weapon
{
    [Header("Mana")]
    [SerializeField] private bool hasManaCost;
    [SerializeField] private int maxManaCost = 10;
    [Header("Projectile")]
    [SerializeField] private bool isProjectile;
    [SerializeField] private Projectile spellProjectile;
    [Header("SelfTargeting")]
    [SerializeField] private bool isSelfTargeting;

    public override void HandleAttack()
    {
        if (CanAttack)
        {
            base.HandleAttack();
            if (IsProjectile)
            {
                this.StartCoroutine(HandleProjectile());
            }
            else if (IsSelfTargeting)
            {
                HandleSelfTargeting();
            }
        }
    }

    IEnumerator HandleProjectile()
    {
        yield return new WaitForSeconds(Stats.StartupSpeed.GetValue());
        Projectile projectileToFire = Instantiate(spellProjectile);

        projectileToFire.transform.position = pointToHitFrom.position;

        Vector3 Forward = pointToHitFrom.position;
        Forward += pointToHitFrom.forward * 2;
        projectileToFire.transform.LookAt(Forward);

        projectileToFire.FireProjectile(Stats.AttackRange.GetValue(),Stats.RandomDamage());
    }

    private void HandleSelfTargeting()
    {

    }

    public bool HasManaCost { get => hasManaCost; }
    public int MaxManaCost { get => maxManaCost; }
    public bool IsProjectile { get => isProjectile; }
    public bool IsSelfTargeting { get => isSelfTargeting; }
}
