using UnityEngine;
using UnityEngine.UI;

public class Weapon : Equipment
{
    [SerializeField] private WeaponStats baseStats;

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, baseStats.AttackRange.GetValue());
    }

    public WeaponStats Stats { get => baseStats;}
}
