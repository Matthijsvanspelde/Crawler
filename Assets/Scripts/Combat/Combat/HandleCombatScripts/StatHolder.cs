using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHolder : MonoBehaviour
{
    [SerializeField] private StatLine stats;

    private float currentHealth = 1;

    private void Awake()
    {
        currentHealth = stats.MaxHealth.GetValue();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > stats.MaxHealth.GetValue())
            currentHealth = stats.MaxHealth.GetValue();
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

    public StatLine Stats { get => stats; }
}
