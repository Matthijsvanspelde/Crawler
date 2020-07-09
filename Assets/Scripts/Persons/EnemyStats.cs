using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStats", menuName = "Stats/EnemyStats")]
public class EnemyStats : StatLine
{
    [SerializeField] Mesh model;
    [SerializeField] AnimationTriggerer animator;

	[SerializeField] private float detectionRadius;

	[SerializeField] private Weapon enemyWeapon;

	public override void Die()
	{
		base.Die();
		animator.SetDeathBool(true);
		Destroy(GetComponentInParent<Transform>().gameObject);
	}

	public float DetectionRadius { get => detectionRadius; }
	public Weapon EnemyWeapon { get => enemyWeapon; }
	public AnimationTriggerer Animator { get => animator; }
}
