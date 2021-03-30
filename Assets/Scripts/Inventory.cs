using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> InventoryItems;
    public Item[] ItemPrefabs;
    private List<int> InventoryItemIDs = new List<int>();

    public static Inventory StaticInventory;
    public InventoryDock Dock;

    private void Start()
    {
        //Singlton
        if(StaticInventory == null)
        {
            DontDestroyOnLoad(gameObject);
            StaticInventory = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    public void AddItem(Item newItem)
    {
        if (!isInInventory(newItem))
        {
            InventoryItems.Add(newItem);
            InventoryItemIDs.Add(newItem.ItemID);
            Dock.AddItems(InventoryItems);
        }
        
    }

    public void RemoveItem(int itemID)
    {
        Item removeItem = FindItem(itemID);
        if (removeItem != null)
        {
            InventoryItems.Remove(removeItem);
            InventoryItemIDs.Remove(itemID);
        }
            
    }

    public Item FindItem(int itemID)
    {
        foreach(Item item in InventoryItems)
        {
            if (item.ItemID == itemID) return item;
        }

        return null;
    }

    private bool isInInventory(Item newItem)
    {
        bool contained = false;
        foreach(Item item in InventoryItems)
        {
            if (item.ItemID == newItem.ItemID) return true;
        }
        return contained;
    }


}
