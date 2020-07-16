using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{    
    private void Update()
    {
        //check input for attack
        if (Input.GetMouseButtonDown(1))
        {
            DoAttack(EquipmentManager.instance.GetEquipmentFromSlot(InventorySlot.RIGHTHAND));
        }
        else if (Input.GetMouseButtonDown(0))
        {
            DoAttack(EquipmentManager.instance.GetEquipmentFromSlot(InventorySlot.LEFTHAND));
        }
    }

    private void DoAttack(Equipment equipment)
    {
        Weapon weaponToAttackWith = null;

        try
        {
            weaponToAttackWith = (Weapon)equipment;
            weaponToAttackWith.HandleAttack(true);
        }
        catch (System.Exception)
        {
            return;
        }
    }
}
