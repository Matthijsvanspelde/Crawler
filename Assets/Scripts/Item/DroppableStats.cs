using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemDroppable", menuName = "Item/DropTable")]
public class DroppableStats : ScriptableObject
{
    [Header("LootTable")]
    [SerializeField] private List<Item> droppableItems = new List<Item>();
    [SerializeField] private List<int> droppablesChance = new List<int>();
    [Range(1,100),Header("ChanceToDrop")]
    [SerializeField] private int chanceToDropItem = 50;

    public Item GetRandomItem()
    {
        int randomInt = Random.Range(0, TotalChance());

        int currentChance = randomInt;

        for (int i = 0; i < droppableItems.Count; i++)
        {
            if (currentChance > randomInt)
            {
                return droppableItems[i];
            }
            else
            {
                currentChance += droppablesChance[i];
            }
        }

        return null;
    }

    private int TotalChance()
    {
        int toReturn = 0;

        foreach (int chance in droppablesChance)
        {
            toReturn += chance;
        }

        return toReturn;
    }

    public int ChanceToDropItem { get => chanceToDropItem; private set => chanceToDropItem = value; }
}
