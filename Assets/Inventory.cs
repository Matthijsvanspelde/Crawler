using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    private Weapon[] equipmentSlots = new Weapon[5];

    private List<Item>() inventoryList = new List<Item>();

    public void EquipItem(Weapon weapon)
    {
        if (equipmentSlots[(int)weapon.AvailableSlots] != null)
        {
            equipmentSlots[(int)weapon.AvailableSlots] = weapon;
        }
    }

    public void UnEquipItem(InventorySlot slotToClear)
    {
        if (equipmentSlots[(int)slotToClear] != null)
        {
            equipmentSlots[(int)slotToClear] = null;
        }
    }
}

public enum InventorySlot{ HEAD, BODY, ARMS, LEGS, LEFTHAND, RIGHTHAND }
