using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTileData", menuName = "Create New TileData")]
public class TileData : ScriptableObject
{
    [SerializeField] private int chanceToSpawn = 10;

    public int ChanceToSpawn { get => chanceToSpawn; }
}
