using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Projectile : MonoBehaviour
{
    [Header("Explosions")]
    [SerializeField] private bool explodeOnImpact;
    [SerializeField] private float explosionRadius;
    [SerializeField] private ParticleSystem explosionGraphics;
    [SerializeField] private LayerMask CanHit;
    [Header("Projectile")]
    [SerializeField] private float projectileSpeed;

    private float damage;
    private float attackRange;

    private Vector3 startPos;

    private bool HasFiredProjectile = false;

    public void FireProjectile(float AttackRange, float damage)
    {
        this.attackRange = AttackRange;
        this.damage = damage;

        startPos = transform.position;
        HasFiredProjectile = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (explodeOnImpact)
        {
            ExplodeProjectile();
        }

        if (Contains(CanHit,other.gameObject.layer))
        {
            if (other.tag == "Player")
            {
                DoPlayerHit();
            }
            else
            {
                DoEnemyHit(other);
            }

            //Destroy projectile
            DestroySelf();
        }
    }

    private void DoPlayerHit()
    {
        PlayerStats.instance.TakeDamage(Mathf.RoundToInt(damage));
    }

    private void DoEnemyHit(Collider other)
    {
        //Do damage
        StatLine enemy = other.GetComponent<StatLine>();
        if (enemy != null)
        {
            enemy.TakeDamage(Mathf.RoundToInt(damage));
        }
    }

    private bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void ExplodeProjectile()
    {
        ParticleSystem tempParticles = Instantiate(explosionGraphics);
        tempParticles.transform.position = transform.position;
        explosionGraphics.Play();

        SphereCollider enemyCollider = new SphereCollider();

        enemyCollider.transform.position = transform.position;
        enemyCollider.radius = explosionRadius;

        DestroySelf();
    }

    private void Update()
    {
        if (HasFiredProjectile)
        {
            transform.position += transform.forward * projectileSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, startPos) >= attackRange)
            {
                DestroySelf();
            }
        }
    }
}