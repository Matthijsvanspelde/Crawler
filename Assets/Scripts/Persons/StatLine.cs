using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatLine : MonoBehaviour
{
    [SerializeField] private Stat maxHealth;
    [SerializeField] private Stat armour;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = Mathf.RoundToInt(maxHealth.GetValue());
    }

    public void TakeDamage(int damage)
    {
        damage -= Mathf.RoundToInt(armour.GetValue());
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, int.MaxValue);
        Debug.Log(transform.name + " takes " + damage + " damage.");

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
}
