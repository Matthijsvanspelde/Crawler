using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponStats stats;
    [SerializeField] private Transform hitPoint;

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(hitPoint.position, stats.AttackRange);
    }

    public WeaponStats Stats { get => stats;}
}
