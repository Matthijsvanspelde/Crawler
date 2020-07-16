using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWeapon : Weapon
{
    [Header("Mana")]
    [SerializeField] private bool hasManaCost;
    [SerializeField] private int ManaCost = 10;
    [Header("Projectile")]
    [SerializeField] private bool isProjectile;
    [SerializeField] private Projectile spellProjectile;
    [Header("SelfTargeting")]
    [SerializeField] private bool isSelfTargeting;

    public override void HandleAttack(bool isPlayer = false)
    {
        if (CanAttack)
        {
            if (isPlayer)
                if (ManaCost > PlayerStats.instance.CurrentMana)
                    return;

            base.HandleAttack(isPlayer);
            if (IsProjectile)
            {
                this.StartCoroutine(HandleProjectile());
            }
            else if (IsSelfTargeting)
            {
                HandleSelfTargeting();
            }

            if (isPlayer)
                HandleManaCost();
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

    private bool CheckManaCost()
    {
        float currentMana = PlayerStats.instance.CurrentMana;

        if (currentMana >= ManaCost)
            return true;
        else
            return false;
    }

    private void HandleManaCost()
    {
        if (hasManaCost)
        {
            PlayerStats.instance.DecreaseCurrentMana(ManaCost);
        }
    }

    public bool HasManaCost { get => hasManaCost; }
    public int MaxManaCost { get => ManaCost; }
    public bool IsProjectile { get => isProjectile; }
    public bool IsSelfTargeting { get => isSelfTargeting; }
}
