using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Tiles")]
    [SerializeField] private List<Tile> tilePrefabs = new List<Tile>();
    [SerializeField] private Tile startTile;
    [SerializeField] private Tile emptyTile;
    [Header("Size")]
    [SerializeField] private int xSize = 4;
    [SerializeField] private int ySize = 4;
    [Header("SpawnSettings")]
    [Range(0,100)]
    [SerializeField] private int EmptySpawnPrecentage = 0;

    private Tile[,] tileGrid;

    private int LoopedAmount = 1;

    private List<Tile> map = new List<Tile>();
    private bool canBeEndPiece = false;

    private bool KeepFilling = true;
    
    // Start is called before the first frame update
    void Start()
    {
        tileGrid = new Tile[xSize, ySize];
        SpawnTile(startTile, new Vector3(0,0,0), (Side)UnityEngine.Random.Range(0,4),xSize / 2, ySize / 2);
        FillMap();
    }

    private void FillMap()
    {
        Tile t = map[0];

        while (KeepFilling)
        {
            CreateTilesAroundTile(t);
            //yield return new WaitForSeconds(1);
            // yield return null;

            t = map[LoopedAmount];
            LoopedAmount++;
            if (LoopedAmount >= map.Count)
            {
                KeepFilling = false;
            }
        }
        //StartCoroutine(testFill());
    }

    //IEnumerator testFill()
    //{
        
    //}

    private bool SpawnEmptyTile()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);

        float rand = UnityEngine.Random.Range(1, 100);
        
        if (rand < EmptySpawnPrecentage)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CreateTilesAroundTile(Tile t)
    {
        for (int SideNumber = 0; SideNumber < 4; SideNumber++)
        {
            float SideDistance = GetSideDistance(t, (Side)SideNumber);

            //if true then we have space to spawn a new tile
            if (SideDistance > 0 && !float.IsNaN(SideDistance))
            {
                Tile ToSpawn = null;

                float ToSpawnSideDistance = 0;

                while (ToSpawnSideDistance == 0)
                {
                    ToSpawn = GetRandomTile();
                    ToSpawnSideDistance = GetSideDistance(ToSpawn, GetOpositeSide((Side)SideNumber));
                }

                t.FillAttachmentPoint((Side)SideNumber);

                Vector3 spawnLocation = CalculateSpawnLocation(t,SideDistance,ToSpawnSideDistance,(Side)SideNumber);

                int xpos = GetxPosFromSide(t, (Side)SideNumber);
                int ypos = GetyPosFromSide(t, (Side)SideNumber);

                if (xpos >= 0 && xpos < xSize && ypos >= 0 && ypos < ySize)
                {
                    if (tileGrid[xpos, ypos] == null)
                    {
                        SpawnTile(ToSpawn, spawnLocation, GetOpositeSide((Side)SideNumber), xpos, ypos);
                    }
                }
            }
        }
    }

    private int GetxPosFromSide(Tile origin, Side s)
    {
        switch (s)
        {
            case Side.TOP:
                return origin.xPos;
            case Side.BOTTOM:
                return origin.xPos;
            case Side.RIGHT:
                return origin.xPos + 1;
            case Side.LEFT:
                return origin.xPos - 1;
            default:
                return 0;
        }
    }

    private int GetyPosFromSide(Tile origin, Side s)
    {
        switch (s)
        {
            case Side.TOP:
                return origin.yPos + 1;
            case Side.BOTTOM:
                return origin.yPos - 1;
            case Side.RIGHT:
                return origin.yPos;
            case Side.LEFT:
                return origin.yPos;
            default:
                return 0;
        }
    }

    private Vector3 CalculateSpawnLocation(Tile t, float sideDistance, float toSpawnSideDistance, Side side)
    {
        Vector3 PosToReturn = Vector3.zero;

        Vector3 TilePos = t.transform.position;

        switch (side)
        {
            case Side.TOP:
                PosToReturn = new Vector3(TilePos.x, TilePos.y, TilePos.z + sideDistance + toSpawnSideDistance);
                break;
            case Side.BOTTOM:
                PosToReturn = new Vector3(TilePos.x, TilePos.y, TilePos.z - sideDistance - toSpawnSideDistance);
                break;
            case Side.RIGHT:
                PosToReturn = new Vector3(TilePos.x + sideDistance + toSpawnSideDistance, TilePos.y, TilePos.z);
                break;
            case Side.LEFT:
                PosToReturn = new Vector3(TilePos.x - sideDistance - toSpawnSideDistance, TilePos.y, TilePos.z);
                break;
            default:
                break;
        }

        return PosToReturn;
    }

    private Side GetOpositeSide(Side s)
    {
        switch (s)
        {
            case Side.TOP:
                return Side.BOTTOM;
            case Side.BOTTOM:
                return Side.TOP;
            case Side.RIGHT:
                return Side.LEFT;
            case Side.LEFT:
                return Side.RIGHT;
            default:
                return Side.BOTTOM;
        }
    }

    private float GetSideDistance(Tile t, Side s)
    {
        switch (s)
        {
            case Side.TOP:
                return t.GetDistanceFromSide(Side.TOP);
            case Side.BOTTOM:
                return t.GetDistanceFromSide(Side.BOTTOM);
            case Side.RIGHT:
                return t.GetDistanceFromSide(Side.RIGHT);
            case Side.LEFT:
                return t.GetDistanceFromSide(Side.LEFT);
            default:
                return 0;
        }
    }

    private void SpawnTile(Tile TileToSpawn, Vector3 SpawnLocation, Side s,int xpos,int ypos)
    {
        GameObject ToSpawn = Instantiate(TileToSpawn.Data.PrefabObject);
        ToSpawn.SetActive(true);
        ToSpawn.transform.position = SpawnLocation;

        Tile tile = ToSpawn.GetComponent<Tile>();

        tile.xPos = xpos;
        tile.yPos = ypos;

        tileGrid[xpos, ypos] = tile;
        map.Add(tile);
    }

    private void AddTileToGrid(Tile tileToAdd)
    {
        List<int> lstXpos = new List<int>();
        List<int> lstYpos = new List<int>();


    }

    private Tile GetRandomTile()
    {
        Tile tileToReturn = tilePrefabs[UnityEngine.Random.Range(0,tilePrefabs.Count)];

        if (!canBeEndPiece)
        {
            return tileToReturn;
        }
        else
        {
            if (tileToReturn.Data.IsEndPiece)
            {
                GetRandomTile();
            }
            else
            {
                return tileToReturn;
            }
        }

        return null;
    }
}
