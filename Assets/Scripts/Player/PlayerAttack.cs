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

    private CombatHandler leftCombatHandler;
    private CombatHandler rightCombatHandler;

    private void Awake()
    {
        rightCombatHandler = rightHandWeapon.GetComponentInParent<CombatHandler>();
        leftCombatHandler = leftHandWeapon.GetComponentInParent<CombatHandler>();
    }

    private void Update()
    {
        //check input for attack
        if (Input.GetMouseButtonDown(1))
        {
            //righthand attack
            if (rightCombatHandler.CanAttack)
            {
                rightHandAnim.SetTrigger("Attack");
            }
            rightCombatHandler.HandleAttack(rightHandWeapon.Stats);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            //lefthand attack
            if (leftCombatHandler.CanAttack)
            {
                rightHandAnim.SetTrigger("Attack");
            }
            leftCombatHandler.HandleAttack(leftHandWeapon.Stats);
        }
    }
}
