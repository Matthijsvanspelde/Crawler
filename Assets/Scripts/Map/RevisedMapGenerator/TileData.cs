﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTileData", menuName = "Create New TileData")]
public class TileData : ScriptableObject
{
    [SerializeField] private int ChanceToSpawn = 100;

    public int ChanceToSpawn1 { get => ChanceToSpawn; }
}
