using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatLine : MonoBehaviour
{
    [Header("HitPoint")]
    [SerializeField] private Transform hitPoint;
    [Header("Stats")]
    [SerializeField] private Stat maxHealth;
    [SerializeField] private Stat armour;
    [Header("Animator")]
    public AnimationTriggerer animator;

    public delegate void HealthChanged(int currentValue);
    public HealthChanged OnHealthChanged;

    public int currentHealth;

    private void Start()
    {
        currentHealth = Mathf.RoundToInt(maxHealth.GetValue());
    }

    public virtual void TakeDamage(int damage)
    {
        damage -= Mathf.RoundToInt(armour.GetValue());
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, int.MaxValue);
        Debug.Log(transform.name + " takes " + damage + " damage.");
        
        animator.SetDamagedTrigger();
        if (currentHealth == 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //Die in some way
        //Overwrite this function
        Debug.Log(transform.name + " died.");
    }
    public Stat MaxHealth { get => maxHealth; }
    public Transform HitPoint { get => hitPoint; }
}
