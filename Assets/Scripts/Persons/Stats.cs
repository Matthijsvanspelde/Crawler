using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : ScriptableObject
{
    [SerializeField] private float maxHealth = 1;

    public float MaxHealth { get => maxHealth; }

}
