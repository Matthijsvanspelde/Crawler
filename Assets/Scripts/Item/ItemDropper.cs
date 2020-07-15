using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemDropper : MonoBehaviour
{
    [SerializeField] private DroppableStats stats;

    /// <summary>
    /// Try to drop an item from pos of currentObject
    /// </summary>
    public void TryDropItem()
    {
        if (CanDrop())
        {
            //We can drop an item
            DropItem();
        }
    }

    private bool CanDrop()
    {
        int chance = Random.Range(0, 100);

        if (stats.ChanceToDropItem >= chance)
        {
            return true;
        }

        return false;
    }

    private Item GetRandomItem()
    {
        Item ToReturn = stats.GetRandomItem();

        return ToReturn;
    }

    private void DropItem()
    {
        Item toSpawn = GetRandomItem();
        Transform SpawnedItem = Instantiate(toSpawn.transform);
        SpawnedItem.transform.position = transform.position;
    }
}
