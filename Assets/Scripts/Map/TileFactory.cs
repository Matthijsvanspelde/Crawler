using UnityEngine;

public class TileFactory : MonoBehaviour
{
    public int openingDir;

    private RoomTemplates roomTemplates;
    private int random;
    [SerializeField]
    private bool spawned = false;

    private void Start()
    {
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    private void Spawn()
    {
        if (!spawned)
        {
            if (openingDir == 1)
            {
                random = Random.Range(0, roomTemplates.BottomRooms.Length);
                Instantiate(roomTemplates.BottomRooms[random], transform.position, roomTemplates.BottomRooms[random].transform.rotation);
            }
            else if (openingDir == 2)
            {
                random = Random.Range(0, roomTemplates.LeftRooms.Length);
                Instantiate(roomTemplates.LeftRooms[random], transform.position, roomTemplates.LeftRooms[random].transform.rotation);
            }
            else if (openingDir == 3)
            {
                random = Random.Range(0, roomTemplates.TopRooms.Length);
                Instantiate(roomTemplates.TopRooms[random], transform.position, roomTemplates.TopRooms[random].transform.rotation);
            }
            else if (openingDir == 4)
            {
                random = Random.Range(0, roomTemplates.RightRooms.Length);
                Instantiate(roomTemplates.RightRooms[random], transform.position, roomTemplates.RightRooms[random].transform.rotation);
            }
            spawned = true;
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttachmentPoint"))
        {
            Destroy(gameObject);
        }
    }
}
