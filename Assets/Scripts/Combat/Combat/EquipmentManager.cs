using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    public delegate void OnEquipmentChange(Equipment oldEquipment, Equipment newEquipment);
    OnEquipmentChange OnEquipmentChangeCallBack;

    private Equipment[] equipmentSlots = new Equipment[5];

    public void EquipEquipment(Equipment equipment)
    {
        if (CheckForEmptySlot(equipment))
            return;

        SwapEquipmentInSlot(equipment);
    }

    private void SwapEquipmentInSlot(Equipment equipment)
    {
        //no empty slot found so we swap item in first available slot
        foreach (InventorySlot slot in equipment.GetAllAvailableSlots)
        {
            if (equipmentSlots[(int)slot] != null)
            {
                //unequip old item in slot
                UnEquipItem(equipmentSlots[(int)slot]);
                //equip new item in current slot
                AddEquipmentInSlot(equipment, slot);
                return;
            }
        }
    }

    private bool CheckForEmptySlot(Equipment equipment)
    {
        foreach (InventorySlot slot in equipment.GetAllAvailableSlots)
        {
            //Check if we have an empty slot for our equipment to equip in
            if (equipmentSlots[(int)slot] == null)
            {
                //equip item in current slot
                AddEquipmentInSlot(equipment, slot);
                return true;
            }
        }

        return false;
    }

    private void AddEquipmentInSlot(Equipment equipment, InventorySlot slot)
    {
        equipment.SetCurrentSlot(slot);
        equipmentSlots[(int)slot] = equipment;
        Inventory.instance.InventoryList.Remove(equipment);
        CallInventoryChangeCallBack(null, equipment);
    }

    public void UnEquipItem(Equipment slotToClear)
    {
        foreach (InventorySlot slot in slotToClear.GetAllAvailableSlots)
        {
            if (slot == slotToClear.GetCurrentSlot())
            {
                RemoveEquipmentFromSlot(slot);
                return;
            }
        }
    }

    private void RemoveEquipmentFromSlot(InventorySlot slotToClear)
    {
        Equipment oldEquipment = equipmentSlots[(int)slotToClear];

        oldEquipment.ClearCurrentSlot();
        equipmentSlots[(int)slotToClear] = null;

        Inventory.instance.InventoryList.Add(oldEquipment);

        CallInventoryChangeCallBack(oldEquipment, null);
    }

    private void CallInventoryChangeCallBack(Equipment oldEquipment, Equipment newEquipment)
    {
        OnEquipmentChangeCallBack.Invoke(oldEquipment, newEquipment);
    }

}

public enum InventorySlot { HEAD, BODY, ARMS, LEGS, LEFTHAND, RIGHTHAND, NONE }
