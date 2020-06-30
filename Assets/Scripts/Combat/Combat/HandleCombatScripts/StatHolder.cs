using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHolder : MonoBehaviour
{
    [SerializeField] private Stats stats;

    private float currentHealth = 1;

    private void Awake()
    {
        currentHealth = Stats.MaxHealth;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > Stats.MaxHealth)
            currentHealth = Stats.MaxHealth;
    }
    
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;

        Debug.Log("HIT -- " + " Damage dealt: " + amount + " -- Current health: " + currentHealth);
    }

    public bool IsDead()
    {
        if (currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Stats Stats { get => stats; }
}
