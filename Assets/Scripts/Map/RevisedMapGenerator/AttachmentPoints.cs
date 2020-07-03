using UnityEngine;

public class AttachmentPoints : MonoBehaviour
{
    public bool Empty = true;

    [SerializeField]private Side spawnSide;

    public Side SpawnSide { get => spawnSide; }
}