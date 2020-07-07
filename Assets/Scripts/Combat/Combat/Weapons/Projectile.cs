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
        if (other.CompareTag("Enemy"))
        {
            //Do damage
            StatLine enemy = other.GetComponent<StatLine>();
            enemy.TakeDamage(Mathf.RoundToInt(damage));
        }

        if (explodeOnImpact)
        {
            ExplodeProjectile();
        }

        if (!other.CompareTag("Player") && !other.CompareTag("Untagged"))
        {
            //Destroy bullet
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void ExplodeProjectile()
    {
        //Make projectile explode
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