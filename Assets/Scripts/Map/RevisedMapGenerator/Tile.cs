using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private TileData data;

    [SerializeField] private Transform middlePoint;

    [SerializeField] private List<AttachmentPoints> points = new List<AttachmentPoints>();

    private Tile leftTile, rightTile, bottomTile, topTile;

    public float Distance(AttachmentPoints point)
    {
        AttachmentPoints p = Points[Points.IndexOf(point)];
        if (p != null)
        {
            return Vector3.Distance(middlePoint.position, p.transform.position);
        }
        else
        {
            return 0;
        }
    }

    public void FillAttachmentPoint(Side s)
    {
        GetPointFromSide(s).Empty = false;
    }

    public AttachmentPoints GetPointFromSide(Side SpawnSide)
    {
        foreach (AttachmentPoints point in Points)
        {
            if (point.Empty && point.SpawnSide == SpawnSide)
            {
                return point;
            }
        }

        return null;
    }

    public float GetDistanceFromSide(Side SpawnSide)
    {
        return Distance(GetPointFromSide(SpawnSide));
    }

    public virtual void InitializeTile()
    {

    }

    public bool HasOpenAttachmentPoints()
    {
        foreach (AttachmentPoints point in Points)
        {
            if (point.Empty)
            {
                return true;
            }
        }

        return false;
    }

    public TileData Data { get => data; }
    public List<AttachmentPoints> Points { get => points; set => points = value; }
}
