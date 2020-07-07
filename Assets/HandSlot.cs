using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSlot : MonoBehaviour
{
    [SerializeField] private InventorySlot slotSide = InventorySlot.LEFTHAND;

    private void Start()
    {
        EquipmentManager.instance.OnEquipmentChangeCallBack += ShowEquipment;
    }

    private Equipment ShowEquipment(Equipment oldEquipment, Equipment newEquipment)
    {
        Equipment NewSpawnedEquipment = null;

        if (newEquipment != null && newEquipment.GetCurrentSlot() == slotSide)
        {
            GameObject go = Instantiate(newEquipment.gameObject, transform);
            go.SetActive(true);
            NewSpawnedEquipment = go.GetComponentInChildren<Equipment>();
        }
        
        if(oldEquipment != null && oldEquipment.GetCurrentSlot() == slotSide)
        {
            Destroy(GetComponentInChildren<Equipment>().gameObject);
        }

        return NewSpawnedEquipment;
    }
}
