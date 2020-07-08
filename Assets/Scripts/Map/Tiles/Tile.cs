using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private TileData data;
    [Header("AttachmentPoints")]
    [SerializeField] private Transform middlePoint;
    [SerializeField] private List<AttachmentPoints> points = new List<AttachmentPoints>();

    [HideInInspector]
    public int xPos;
    [HideInInspector]
    public int yPos;

    public int xSize;
    public int ySize;

    public float Distance(AttachmentPoints point)
    {
        int index = Points.IndexOf(point);
        if (index >= 0 && index <= Points.Count)
        {
            AttachmentPoints p = Points[index];
            if (p != null)
            {
                return Vector3.Distance(middlePoint.position, p.transform.position);
            }
        }
        return 0;
    }

    public void FillAttachmentPoint(Side s)
    {
        AttachmentPoints at = GetPointFromSide(s);

        if(at !=null)
            at.Empty = false;
    }

    public AttachmentPoints GetPointFromSide(Side SpawnSide)
    {
        foreach (AttachmentPoints point in Points)
        {
            if (point.SpawnSide == SpawnSide)
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
