using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "Stats/PlayerStats")]
public class PlayerStats : Stats
{
    [SerializeField] private float maxMana;
    [SerializeField] private float maxStamina;

    public float MaxMana { get => maxMana; }
    public float MaxStamina { get => maxStamina; }
}
