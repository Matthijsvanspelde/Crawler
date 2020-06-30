using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMagicWeapon", menuName = "WeaponStats/Magic")]
public class Magic : WeaponStats
{
    [Header("Mana")]
    [SerializeField] private bool hasManaCost;
    [SerializeField] private int maxManaCost = 10;
    [Header("Props")]
    [SerializeField] private bool isProjectile;
    [SerializeField] private bool isSelfTargeting;
    [SerializeField] private bool hasExplosion;
    [SerializeField] private float explosionRadius = 0f;

    public bool HasManaCost { get => hasManaCost; }
    public int MaxManaCost { get => maxManaCost; }
    public bool IsProjectile { get => isProjectile; }
    public bool IsSelfTargeting { get => isSelfTargeting; }
    public bool HasExplosion { get => hasExplosion; }
    public float ExplosionRadius { get => explosionRadius; }
}
