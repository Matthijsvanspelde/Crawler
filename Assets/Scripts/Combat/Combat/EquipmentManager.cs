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

    public delegate Equipment OnEquipmentChange(Equipment oldEquipment, Equipment newEquipment);

    public OnEquipmentChange OnEquipmentChangeCallBack;

    [SerializeField] private List<Equipment> startingEquipment = new List<Equipment>();

    private Equipment[] equipmentSlots = new Equipment[6];

    private void Start()
    {
        foreach (Equipment equipment in startingEquipment)
        {
            EquipEquipment(equipment);
        }
    }

    public Equipment GetEquipmentFromSlot(InventorySlot slotToGet)
    {
        return equipmentSlots[(int)slotToGet];
    }

    public void EquipEquipment(Equipment equipment)
    {
        if (equipment != null)
        {
            if (CheckForEmptySlot(equipment))
                return;

            SwapEquipmentInSlot(equipment);
        }
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
                StartCoroutine(AddEquipmentInSlot(equipment, slot));
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
                StartCoroutine(AddEquipmentInSlot(equipment, slot));
                return true;
            }
        }

        return false;
    }

    private IEnumerator AddEquipmentInSlot(Equipment equipment, InventorySlot slot)
    {
        equipment.SetCurrentSlot(slot);
        Equipment SpawnedEquipment = CallInventoryChangeCallBack(null, equipment);
        yield return null;
        if (SpawnedEquipment != null)
        {
            SpawnedEquipment.SetCurrentSlot(slot);
            equipmentSlots[(int)slot] = SpawnedEquipment;
            Inventory.instance.InventoryList.Remove(SpawnedEquipment);
        }
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

    private Equipment CallInventoryChangeCallBack(Equipment oldEquipment, Equipment newEquipment)
    {
        return OnEquipmentChangeCallBack.Invoke(oldEquipment,newEquipment);
    }

    public Equipment[] EquipmentSlots { get => equipmentSlots; }
}

public enum InventorySlot { HEAD, BODY, ARMS, LEGS, LEFTHAND, RIGHTHAND, NONE }
