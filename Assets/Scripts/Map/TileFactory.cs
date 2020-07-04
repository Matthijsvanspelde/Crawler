using UnityEngine;

public class TileFactory : MonoBehaviour
{
    public Side openingDir;

    private RoomTemplates roomTemplates;
    private int random;
    [SerializeField]
    private bool spawned = false;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (!spawned)
        {
            random = Random.Range(0, roomTemplates.RightRooms.Length);

            GameObject roomToSpawn = GetRandomRoomFromSide(openingDir);

            Instantiate(roomToSpawn, transform.position, roomToSpawn.transform.rotation);
            spawned = true;
        }
    }

    private GameObject GetRandomRoomFromSide(Side openingDir)
    {
        GameObject toreturn;

        switch (openingDir)
        {
            case Side.TOP:
                toreturn = roomTemplates.TopRooms[random];
                break;
            case Side.BOTTOM:
                toreturn = roomTemplates.BottomRooms[random];
                break;
            case Side.RIGHT:
                toreturn = roomTemplates.RightRooms[random];
                break;
            case Side.LEFT:
                toreturn = roomTemplates.LeftRooms[random];
                break;
            default:
                toreturn = roomTemplates.RightRooms[random];
                break;
        }

        return toreturn;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("AttachmentPoint"))
    //    {
    //        if (!other.GetComponent<TileFactory>().spawned && !spawned)
    //        {
    //            Instantiate(roomTemplates.closedRoom, transform.position, Quaternion.identity);
    //            Destroy(gameObject);
    //        }
    //        spawned = true;
    //    }
    //}
}
