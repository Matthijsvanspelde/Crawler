﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : ScriptableObject
{
    [Header("Damage")]
    [SerializeField] private float minAttackDamage = 0;
    [SerializeField] private float maxAttackDamage = 4;

    [Header("Range"), Range(0.1f,100)]
    [SerializeField] private float attackRange = 10;

    [Header("Speed")]
    [SerializeField] private float attackSpeed = 0.2f;
    [SerializeField] private float startupSpeed;

    [Header("Cost")]
    [SerializeField] private float staminaCost;

    public virtual void HandleWeapon()
    {
        //TODO: Make stamina work
    }

    public float MinAttackDamage { get => minAttackDamage;}
    public float AttackRange { get => attackRange;}
    public float AttackSpeed { get => attackSpeed;}
    public float StartupSpeed { get => startupSpeed;}
    public float StaminaCost { get => staminaCost;}
    public float MaxAttackDamage { get => maxAttackDamage; }
    public float RandomDamage() { return Mathf.Round(Random.Range(minAttackDamage, maxAttackDamage)); }
}
