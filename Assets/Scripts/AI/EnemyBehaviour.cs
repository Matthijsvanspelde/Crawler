using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyBehaviour", menuName = "AI/EnemyBehaviour")]
public class EnemyBehaviour : ScriptableObject
{
	[SerializeField] private AIState enemyState;

	public AIState EnemyState { get => enemyState; }

}
