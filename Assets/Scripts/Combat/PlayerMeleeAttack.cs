using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    public Camera cam;
    public GameObject Hand;
    public Weapon meleeWeapon;
    private Animator handAnim;

    private float attackTimerMelee;

    void Start()
    {
        handAnim = Hand.GetComponent<Animator>();
        meleeWeapon = Hand.GetComponentInChildren<Weapon>();
        handAnim.speed /= meleeWeapon.attackSpeed;
        attackTimerMelee = meleeWeapon.attackSpeed;     
    }


    void Update()
    {
        SlashAttack();
    }

    private void SlashAttack() 
    {
        attackTimerMelee += Time.deltaTime;
        if (Input.GetMouseButtonUp(1) && attackTimerMelee >= meleeWeapon.attackSpeed)
        {
            attackTimerMelee = 0f;
            handAnim.SetTrigger("Attack");
            DoAttack(meleeWeapon.attackStabRange, meleeWeapon.attackSlashDamage);
        }
    }

    private void StabAttack() 
    { 
        //Not yet implemented
    }

    private void DoAttack(float range, float damage)
    {

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.tag == "Enemy")
            {
                EnemyHealth eHealth = hit.collider.GetComponent<EnemyHealth>();
                eHealth.TakeDamage(damage);
            }
        }

    }
}
