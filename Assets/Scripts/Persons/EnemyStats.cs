using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStats", menuName = "Stats/EnemyStats")]
public class EnemyStats : Stats
{
    [SerializeField] Mesh model;
    [SerializeField] float detectionRadius;
}
