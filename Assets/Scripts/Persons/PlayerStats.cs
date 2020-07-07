using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "Stats/PlayerStats")]
public class PlayerStats : StatLine
{
    [SerializeField] private Stat maxMana;
    [SerializeField] private Stat maxStamina;

    public Stat MaxMana { get => maxMana; }
    public Stat MaxStamina { get => maxStamina; }
}
