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
    [Header("Weapons")]
    [SerializeField]
    private Weapon rightHandWeapon;
    [SerializeField]
    private Weapon leftHandWeapon;

    private void Update()
    {
        //check input for attack
        if (Input.GetMouseButtonDown(1))
        {
            //righthand attack
            if (rightHandWeapon.CanAttack)
            {
                rightHandAnim.SetTrigger("Attack");
            }

            rightHandWeapon.HandleAttack();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            //lefthand attack
            if (leftHandWeapon.CanAttack)
            {
                leftHandAnim.SetTrigger("Attack");
            }

            leftHandWeapon.HandleAttack();
        }
    }
}
