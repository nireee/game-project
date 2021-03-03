using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> InventoryItems;

    public void AddItem(Item newItem)
    {
        if (!isInInventory(newItem)) InventoryItems.Add(newItem);
    }

    public void RemoveItem(GameObject itemPrefab)
    {
        Item removeItem = FindItem(itemPrefab);
        if (removeItem != null) InventoryItems.Remove(removeItem);
    }

    public Item FindItem(GameObject itemPrefab)
    {
        foreach(Item item in InventoryItems)
        {
            if (item.ItemPrefab == itemPrefab) return item;
        }

        return null;
    }

    private bool isInInventory(Item newItem)
    {
        bool contained = false;
        foreach(Item item in InventoryItems)
        {
            if (item.ItemPrefab == newItem.ItemPrefab) return true;
        }
        return contained;
    }


}
