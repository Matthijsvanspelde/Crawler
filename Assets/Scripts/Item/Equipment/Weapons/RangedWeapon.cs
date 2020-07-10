using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RangedWeapon : Weapon
{
    [Header("Ammo")]
    [SerializeField] private bool usesAmmo;
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private int ammoUsedPerShot = 1;

    public bool UsesAmmo { get => usesAmmo; }
    public int MaxAmmo { get => maxAmmo; }
    public int AmmoUsedPerShot { get => ammoUsedPerShot; }
}
