using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : StatLine
{
	[Header("Numbers")]
	[SerializeField] private float detectionRadius;
	[SerializeField] private float deathDelay = 1;

	[Header("Weapon")]
	[SerializeField] private Weapon enemyWeapon;
	[Header("Loot")]
	[SerializeField] private ItemDropper LootTable;

	public override void Die()
	{
		base.Die();
		Debug.Log("deathDelay" + deathDelay);
		animator.SetDeathBool(true);
		TryDropItem();
		StartCoroutine(DestroySelf());
	}

	private void TryDropItem()
	{
		if (LootTable != null)
		{
			LootTable.TryDropItem();
		}
	}

	private IEnumerator DestroySelf()
	{
		yield return new WaitForSeconds(deathDelay);
		Destroy(transform.parent.gameObject);
	}

	public float DetectionRadius { get => detectionRadius; }
	public Weapon EnemyWeapon { get => enemyWeapon; }
}
