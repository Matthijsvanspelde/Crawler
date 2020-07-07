using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWeapon : Weapon
{
    [Header("Mana")]
    [SerializeField] private bool hasManaCost;
    [SerializeField] private int maxManaCost = 10;
    [Header("Props")]
    [SerializeField] private bool isProjectile;
    [SerializeField] private bool isSelfTargeting;

    public bool HasManaCost { get => hasManaCost; }
    public int MaxManaCost { get => maxManaCost; }
    public bool IsProjectile { get => isProjectile; }
    public bool IsSelfTargeting { get => isSelfTargeting; }
}
