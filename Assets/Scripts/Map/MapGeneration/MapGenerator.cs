using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SideCalculator))]
public class MapGenerator : MonoBehaviour
{
    [Header("Tiles")]
    [SerializeField] private List<Tile> tilePrefabsList = new List<Tile>();
    [SerializeField] private Tile startTile = null;
    [Header("Size")]
    [SerializeField] private int xSize = 4;
    [SerializeField] private int ySize = 4;

    private Tile[,] tileMapGrid;
    private List<Tile> runThroughMap = new List<Tile>();
    private List<Tile> tileChanceList = new List<Tile>();

    private SideCalculator sideCalc;

    private int LoopedAmount = 1;
    private int TotalChance = 0;
    private int MaxAmountOfTryToGetTile = 1000;

    private bool KeepFilling = true;

    private void Awake()
    {
        sideCalc = GetComponent<SideCalculator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        tileMapGrid = new Tile[xSize, ySize];
        SpawnTile(startTile, new Vector3(0,0,0), (Side)UnityEngine.Random.Range(0,4),xSize / 2, ySize / 2);
        FillMap();
        SetupAllTiles();
    }

    private void SetupAllTiles()
    {
        foreach (Tile tile in tileMapGrid)
        {
            if (tile != null)
            {
                tile.InitializeTile();
            }
        }
    }

    private void FillMap()
    {
        Tile tileToStartFrom = runThroughMap[0];

        while (KeepFilling)
        {
            CreateTilesAroundTile(tileToStartFrom);
            //yield return new WaitForSeconds(2);
            // yield return null;

            tileToStartFrom = runThroughMap[LoopedAmount];
            LoopedAmount++;
            if (LoopedAmount >= runThroughMap.Count)
            {
                KeepFilling = false;
            }
        }
    }
    
    private void CreateTilesAroundTile(Tile TileToStartFrom)
    {
        for (int SideNumber = 0; SideNumber < 4; SideNumber++)
        {
            FillSide(TileToStartFrom, (Side)SideNumber);
        }
    }

    private void FillSide(Tile TileToStartFrom, Side sideToSpawnAt)
    {
        float SideDistance = sideCalc.GetSideDistance(TileToStartFrom, sideToSpawnAt);

        //if true then we have space to spawn a new tile
        if (SideDistance > 0 && !float.IsNaN(SideDistance))
        {
            Tile TileToSpawn = null;
            float ToSpawnSideDistance = GetSpawnDistanceAndRandomTile(sideToSpawnAt, ref TileToSpawn);

            Vector3 spawnLocation = sideCalc.CalculateSpawnLocation(TileToStartFrom, SideDistance, ToSpawnSideDistance, sideToSpawnAt);

            int xpos = sideCalc.GetxPosFromSide(TileToStartFrom, sideToSpawnAt);
            int ypos = sideCalc.GetyPosFromSide(TileToStartFrom, sideToSpawnAt);

            if (xpos >= 0 && xpos < xSize && ypos >= 0 && ypos < ySize)
            {
                if (tileMapGrid[xpos, ypos] == null)
                {
                    SpawnTile(TileToSpawn, spawnLocation, sideCalc.GetOpositeSide(sideToSpawnAt), xpos, ypos);
                }
            }
        }
    }

    private float GetSpawnDistanceAndRandomTile(Side side, ref Tile ToSpawn)
    {
        float ToSpawnSideDistance = 0;

        int i = 0;

        bool keepSearching = true;

        while (keepSearching)
        {
            ToSpawn = GetRandomTile();
            ToSpawnSideDistance = sideCalc.GetSideDistance(ToSpawn, sideCalc.GetOpositeSide((Side)side));
            i++;

            //No Tile Found after max amount of try's
            if (i > MaxAmountOfTryToGetTile)
                keepSearching = false;

            //Found tile that fits
            if (ToSpawnSideDistance != 0)
                keepSearching = false;
        }

        return ToSpawnSideDistance;
    }

    private void SpawnTile(Tile TileToSpawn, Vector3 SpawnLocation, Side s,int xpos,int ypos)
    {
        GameObject ToSpawn = Instantiate(TileToSpawn.gameObject);
        SetGameObject(SpawnLocation, ToSpawn);

        Tile tile = ToSpawn.GetComponent<Tile>();
        SetTile(xpos, ypos, tile);

        tileMapGrid[xpos, ypos] = tile;
        runThroughMap.Add(tile);
    }

    private void SetTile(int xpos, int ypos, Tile tile)
    {
        tile.xPos = xpos;
        tile.yPos = ypos;
    }

    private void SetGameObject(Vector3 SpawnLocation, GameObject TileToSpawn)
    {
        TileToSpawn.SetActive(true);
        TileToSpawn.transform.position = SpawnLocation;
        TileToSpawn.transform.parent = transform;
    }

    private Tile GetRandomTile()
    {
        if (TotalChance == 0)
            SetTotalChance();

        int randomIndex = UnityEngine.Random.Range(0, TotalChance);

        Tile RandomTileFromChanceList = tileChanceList[randomIndex];
        return RandomTileFromChanceList;
    }

    private void SetTotalChance()
    {
        foreach (Tile tile in tilePrefabsList)
        {
            for (int i = 0; i < tile.Data.ChanceToSpawn; i++)
            {
                TotalChance++;
                tileChanceList.Add(tile);
            }
        }
    }
}
