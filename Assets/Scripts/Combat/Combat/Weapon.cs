using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponStats baseStats;
    [SerializeField] private InventorySlot availableSlots;

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, baseStats.AttackRange.GetValue());
    }

    public WeaponStats Stats { get => baseStats;}
    public InventorySlot AvailableSlots { get => availableSlots; }
}
