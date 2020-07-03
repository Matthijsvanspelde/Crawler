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
    [SerializeField] private int AmountToLoop = 4;
    [Header("SpawnSettings")]
    [Range(0,100)]
    [SerializeField] private int EmptySpawnPrecentage = 0;

    private int LoopedAmount = 0;

    private List<Tile> map = new List<Tile>();
    private bool canBeEndPiece = false;

    private bool KeepFilling = true;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnTile(startTile, new Vector3(0,0,0));
        FillMap();
    }

    private void FillMap()
    {
        StartCoroutine(testFill());
    }

    IEnumerator testFill()
    {
        Tile t = map[0];

        while (KeepFilling)
        {
            CreateTilesAroundTile(t);
            yield return new WaitForSeconds(3f);
            LoopedAmount++;
            t = map[LoopedAmount];

            if (LoopedAmount >= AmountToLoop)
            {
                KeepFilling = false;
            }
        }
    }

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
            if (SideDistance > 0)
            {
                Tile ToSpawn = null;

                t.FillAttachmentPoint((Side)SideNumber);

                float ToSpawnSideDistance = 0;

                while (ToSpawnSideDistance == 0)
                {
                    ToSpawn = GetRandomTile();
                    ToSpawnSideDistance = GetSideDistance(ToSpawn, GetOpositeSide((Side)SideNumber));
                }

                Vector3 spawnLocation = CalculateSpawnLocation(t,SideDistance,ToSpawnSideDistance,(Side)SideNumber);

                if (SpawnEmptyTile())
                {
                    SpawnTile(emptyTile, spawnLocation);
                }
                else
                {
                    SpawnTile(ToSpawn, spawnLocation);
                }
            }
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

    private void SpawnTile(Tile TileToSpawn, Vector3 SpawnLocation)
    {
        GameObject ToSpawn = Instantiate(TileToSpawn.Data.PrefabObject);
        ToSpawn.SetActive(true);
        ToSpawn.transform.position = SpawnLocation;

        Tile tile = ToSpawn.GetComponent<Tile>();

        AttachTile(tile);

        map.Add(tile);
    }

    private void AttachTile(Tile t)
    {
        for (int SideNumber = 0; SideNumber < 4; SideNumber++)
        {
            if (ShootRay((Side)SideNumber, t))
            {
                t.FillAttachmentPoint((Side)SideNumber);
            }
        }
    }

    private bool ShootRay(Side s, Tile t)
    {
        Vector3 tempVector = t.GetPointFromSide(s).transform.position;


        Ray ray = new Ray(tempVector, t.GetPointFromSide(s).transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, t.Distance(t.GetPointFromSide(s))))
        {
            hit.collider.GetComponentInParent<Tile>().FillAttachmentPoint(s);
            return true;
        }
        else
        {
            return false;
        }
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
