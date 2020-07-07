using UnityEngine;

public class Equipment : Item
{
    [SerializeField] private InventorySlot[] availableSlots;

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

}