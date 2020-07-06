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
    [SerializeField] private Tile startTile;
    [Header("Size")]
    [SerializeField] private int xSize = 4;
    [SerializeField] private int ySize = 4;

    private Tile[,] tileMapGrid;
    private List<Tile> runThroughMap = new List<Tile>();
    private List<Tile> tileChanceList = new List<Tile>();

    private SideCalculator sideCalc;

    private int LoopedAmount = 1;
    private int TotalChance = 0;

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
        
    }

    private void FillMap()
    {
        Tile t = runThroughMap[0];

        while (KeepFilling)
        {
            CreateTilesAroundTile(t);
            //yield return new WaitForSeconds(2);
            // yield return null;

            t = runThroughMap[LoopedAmount];
            LoopedAmount++;
            if (LoopedAmount >= runThroughMap.Count)
            {
                KeepFilling = false;
            }
        }
    }
    
    private void CreateTilesAroundTile(Tile t)
    {
        for (int SideNumber = 0; SideNumber < 4; SideNumber++)
        {
            FillSide(t, (Side)SideNumber);
        }
    }

    private void FillSide(Tile t, Side sideToSpawnAt)
    {
        float SideDistance = sideCalc.GetSideDistance(t, sideToSpawnAt);

        //if true then we have space to spawn a new tile
        if (SideDistance > 0 && !float.IsNaN(SideDistance))
        {
            Tile ToSpawn = null;
            float ToSpawnSideDistance = GetSpawnDistance(sideToSpawnAt, ref ToSpawn);

            Vector3 spawnLocation = sideCalc.CalculateSpawnLocation(t, SideDistance, ToSpawnSideDistance, (Side)sideToSpawnAt);

            int xpos = sideCalc.GetxPosFromSide(t, (Side)sideToSpawnAt);
            int ypos = sideCalc.GetyPosFromSide(t, (Side)sideToSpawnAt);

            if (xpos >= 0 && xpos < xSize && ypos >= 0 && ypos < ySize)
            {
                if (tileMapGrid[xpos, ypos] == null)
                {
                    SpawnTile(ToSpawn, spawnLocation, sideCalc.GetOpositeSide((Side)sideToSpawnAt), xpos, ypos);
                }
            }
        }
    }

    private float GetSpawnDistance(Side side, ref Tile ToSpawn)
    {
        float ToSpawnSideDistance = 0;

        while (ToSpawnSideDistance == 0)
        {
            ToSpawn = GetRandomTile();
            ToSpawnSideDistance = sideCalc.GetSideDistance(ToSpawn, sideCalc.GetOpositeSide((Side)side));
        }

        return ToSpawnSideDistance;
    }

    private void SpawnTile(Tile TileToSpawn, Vector3 SpawnLocation, Side s,int xpos,int ypos)
    {
        GameObject ToSpawn = Instantiate(TileToSpawn.PrefabObject);
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

    private void SetGameObject(Vector3 SpawnLocation, GameObject ToSpawn)
    {
        ToSpawn.SetActive(true);
        ToSpawn.transform.position = SpawnLocation;
        ToSpawn.transform.parent = transform;
    }

    private Tile GetRandomTile()
    {
        if (TotalChance == 0)
            SetTotalChance();

        int randomIndex = UnityEngine.Random.Range(0, TotalChance);

        Tile tileToReturn = tileChanceList[randomIndex];
        return tileToReturn;
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
