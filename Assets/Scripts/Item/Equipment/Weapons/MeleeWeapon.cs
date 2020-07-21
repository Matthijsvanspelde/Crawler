using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [Range(0, 360)]
    [SerializeField] private float attackAngle;

    private bool doAttack = false;

    private void Awake()
    {
        SetTrigger();
    }

    private void SetTrigger()
    {
        SphereCollider triggerCollider = GetComponent<SphereCollider>();
        triggerCollider.isTrigger = true;
        triggerCollider.radius = Stats.AttackRange.GetValue();
    }

    public override void HandleAttack(bool isPlayer = false)
    {
        if (CanAttack)
        {
            base.HandleAttack(isPlayer);
            StartCoroutine(DoAttack(isPlayer));
        }
    }

    public IEnumerator DoAttack(bool isplayer)
    {
        yield return new WaitForSeconds(Stats.StartupSpeed.GetValue());
        doAttack = true;

        HandleWeapon();
    }

    private void HandleHit(Collider col)
    {
        StatLine HitStatHolder = col.GetComponent<StatLine>();

        //Make sure we did not hit something NoneHitable
        if (HitStatHolder != null)
        {
            if (HitStatHolder.transform.CompareTag("Player"))
            {
                 PlayerStats.instance.TakeDamage(Stats.RandomDamage());
            }
            else
            {
                HitStatHolder.TakeDamage(Stats.RandomDamage());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (doAttack)
        {
            doAttack = false;
            Vector3 rayEndPoint = other.transform.position - transform.position;
            float distance = Vector3.Distance(other.transform.position, transform.position);
            float angle = Vector3.Angle(rayEndPoint, transform.forward);
            if (angle < attackAngle - 30 && distance < Stats.AttackRange.GetValue())
            {
                if (IsHit(rayEndPoint))
                {
                    HandleHit(other);
                }
            }
        }
    }

    private bool IsHit(Vector3 playerPos)
    {
        Ray ray = new Ray(transform.position, playerPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Stats.AttackRange.GetValue(), CanHit))
        {
            Debug.DrawRay(transform.position, playerPos, Color.blue, 5f);
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        float totalFOV = attackAngle;
        float rayRange = Stats.AttackRange.GetValue();
        float halfFOV = totalFOV / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);
    }
}