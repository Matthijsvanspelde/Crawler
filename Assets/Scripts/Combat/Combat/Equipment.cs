using UnityEngine;

public class Equipment : Item
{
    [SerializeField] private InventorySlot[] availableSlots;
    [SerializeField] private GameObject prefabEquipment;

    private InventorySlot currentSlot;

    public void SetCurrentSlot(InventorySlot slot)
    {
        currentSlot = slot;
    }

    public InventorySlot GetCurrentSlot()
    {
        return currentSlot;
    }

    public void ClearCurrentSlot()
    {
        currentSlot = InventorySlot.NONE;
    }

    public InventorySlot[] GetAllAvailableSlots { get => availableSlots; }
    public GameObject PrefabEquipment { get => prefabEquipment; }
}