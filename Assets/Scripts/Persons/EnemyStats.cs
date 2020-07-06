using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStats", menuName = "Stats/EnemyStats")]
public class EnemyStats : StatLine
{
    [SerializeField] Mesh model;

	[SerializeField] private float detectionRadius;

	public float DetectionRadius { get => detectionRadius; set => detectionRadius = value; }
}
