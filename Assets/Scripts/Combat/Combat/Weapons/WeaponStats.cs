using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponStats
{
    [Header("Damage")]
    [SerializeField] private Stat minAttackDamage;
    [SerializeField] private Stat maxAttackDamage;

    [Header("Range")]
    [SerializeField] private Stat attackRange;

    [Header("Speed")]
    [SerializeField] private Stat attackSpeed;
    [SerializeField] private Stat startupSpeed;

    [Header("Cost")]
    [SerializeField] private Stat staminaCost;

    public virtual void HandleWeapon()
    {
        //TODO: Make stamina work
    }

    public Stat MinAttackDamage { get => minAttackDamage;}
    public Stat AttackRange { get => attackRange;}
    public Stat AttackSpeed { get => attackSpeed;}
    public Stat StartupSpeed { get => startupSpeed;}
    public Stat StaminaCost { get => staminaCost;}
    public Stat MaxAttackDamage { get => maxAttackDamage; }
    public int RandomDamage() { return Mathf.RoundToInt(Random.Range(minAttackDamage.GetValue(), maxAttackDamage.GetValue())); }
}
