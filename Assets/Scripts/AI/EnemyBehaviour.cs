using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyBehaviour", menuName = "AI/EnemyBehaviour")]
public class EnemyBehaviour : ScriptableObject
{
	[SerializeField] private State enemyState;

	public State EnemyState { get => enemyState; }

}
