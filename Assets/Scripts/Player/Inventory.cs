using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    [SerializeField]
    private int MaxInventorySpace = 10;

    private List<Item> inventoryList = new List<Item>();
    
    public void AddItemToInventory(Item toAdd)
    {
        inventoryList.Add(toAdd);
    }

    public void RemoveItemToInventory(Item toRemove)
    {
        inventoryList.Remove(toRemove);
    }

    public List<Item> InventoryList { get => inventoryList; }
}

