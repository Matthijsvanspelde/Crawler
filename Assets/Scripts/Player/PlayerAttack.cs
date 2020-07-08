using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField]
    private Animator rightHandAnim;
    [SerializeField]
    private Animator leftHandAnim;
    
    private void Update()
    {
        //check input for attack
        if (Input.GetMouseButtonDown(1))
        {
            DoAttack(EquipmentManager.instance.GetEquipmentFromSlot(InventorySlot.RIGHTHAND), rightHandAnim);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            DoAttack(EquipmentManager.instance.GetEquipmentFromSlot(InventorySlot.LEFTHAND), leftHandAnim);
        }
    }

    private void DoAttack(Equipment equipment, Animator attackanimator)
    {
        Weapon weaponToAttackWith = null;

        try
        {
            weaponToAttackWith = (Weapon)equipment;
            weaponToAttackWith.HandleAttack(attackanimator);
        }
        catch (System.Exception)
        {
            return;
        }
    }
}
