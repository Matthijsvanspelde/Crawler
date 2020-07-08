using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCalculator : MonoBehaviour
{
    public int GetxPosFromSide(Tile origin, Side s)
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

    public int GetyPosFromSide(Tile origin, Side s)
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

    public Vector3 CalculateSpawnLocation(Tile t, float sideDistance, float toSpawnSideDistance, Side side)
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

    public Side GetOpositeSide(Side s)
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

    public float GetSideDistance(Tile t, Side s)
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
}
