using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : ScriptableObject
{
    [Header("Damage")]
    [SerializeField] private float attackDamage;

    [Header("Range"), Range(0.1f,100)]
    [SerializeField] private float attackRange;

    [Header("Speed")]
    [SerializeField] private float attackSpeed;
    [SerializeField] private float startupSpeed;

    [Header("Cost")]
    [SerializeField] private float staminaCost;

    public float AttackDamage { get => attackDamage;}
    public float AttackRange { get => attackRange;}
    public float AttackSpeed { get => attackSpeed;}
    public float StartupSpeed { get => startupSpeed;}
    public float StaminaCost { get => staminaCost;}
}
