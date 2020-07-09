using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : Equipment
{
    [Header("Basic weapon stats")]
    [SerializeField] private WeaponStats baseStats;

    [Header("Animations")]
    public AnimationTriggerer animator;

    [Header("Hittables")]
    public LayerMask CanHit;

    [Header("HitPoint")]
    public Transform pointToHitFrom;

    private float currentAttackSpeedTimer = 0f;

    private void Awake()
    {
        if (pointToHitFrom == null)
        {
            pointToHitFrom = HitpointMover.instance.transform;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, baseStats.AttackRange.GetValue());
    }

    public virtual void HandleWeapon()
    {

    }

    public virtual void HandleAttack()
    {
        if (CanAttack)
        {
            if (animator == null)
                SetAnimator();
            
            animator.SetCombatTrigger();
            StartCoroutine(AttackSpeedTimer());
        }
    }

    private void SetAnimator()
    {
        animator = GetComponentInParent<Transform>().GetComponentInParent<AnimationTriggerer>();
    }

    public IEnumerator AttackSpeedTimer()
    {
        CanAttack = false;

        while (CanAttack == false)
        {
            currentAttackSpeedTimer += Time.deltaTime;

            if (currentAttackSpeedTimer >= baseStats.AttackSpeed.GetValue())
            {
                currentAttackSpeedTimer = 0f;
                CanAttack = true;
            }
            yield return null;
        }

        yield return null;
    }

    public bool CanAttack { get; private set; } = true;

    public WeaponStats Stats { get => baseStats;}
}
