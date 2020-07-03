using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTileData", menuName = "Create New TileData")]
public class TileData : ScriptableObject
{
    [SerializeField] private bool isEmptyTile = false;
    [SerializeField] private bool isEndPiece = false;

    [SerializeField] private GameObject prefabObject;

    

    public bool IsEmptyTile { get => isEmptyTile; }
    public bool IsEndPiece { get => isEndPiece; }
    public GameObject PrefabObject { get => prefabObject; }
}
