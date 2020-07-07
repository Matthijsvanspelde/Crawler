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
            rightHandAnim.SetTrigger("Attack");
            DoAttack(EquipmentManager.instance.GetEquipmentFromSlot(InventorySlot.RIGHTHAND));
        }
        else if (Input.GetMouseButtonDown(0))
        {
            leftHandAnim.SetTrigger("Attack");
            DoAttack(EquipmentManager.instance.GetEquipmentFromSlot(InventorySlot.LEFTHAND));
        }
    }

    private void DoAttack(Equipment equipment)
    {
        Weapon weaponToAttackWith = null;

        try
        {
            weaponToAttackWith = (Weapon)equipment;
            weaponToAttackWith.HandleAttack();
        }
        catch (System.Exception)
        {
            return;
        }
    }
}
