using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Camera cam;
    public GameObject Hand;
    public Weapon myWeapon;
    Animator handAnim;

    private float attackTimer;

    void Start()
    {
        handAnim = Hand.GetComponent<Animator>();
        myWeapon = Hand.GetComponentInChildren<Weapon>();
        handAnim.speed = handAnim.speed / myWeapon.attackSpeed;
        attackTimer = myWeapon.attackSpeed;
    }


    void Update()
    {
        CheckReady();
    }

    private void CheckReady() 
    {
        attackTimer += Time.deltaTime;
        if (Input.GetMouseButtonUp(0) && attackTimer >= myWeapon.attackSpeed)
        {
            attackTimer = 0f;
            handAnim.SetTrigger("Attack");
            DoAttack();
        }
    }

    private void DoAttack()
    {

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, myWeapon.attackRange))
        {
            if (hit.collider.tag == "Enemy")
            {
                EnemyHealth eHealth = hit.collider.GetComponent<EnemyHealth>();
                eHealth.TakeDamage(myWeapon.attackDamage);
            }
        }

    }
}
