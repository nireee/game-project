﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<Item> InventoryItems;
    public Item[] ItemPrefabs;
    public List<int> InventoryItemIDs;
    public InventoryDock Dock;

    public static Inventory StaticInventory;


    private void Start(){
        // Singlton
        if(StaticInventory == null)
        {
            DontDestroyOnLoad(gameObject);
            StaticInventory = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    public void AddItem(Item newItem){
       if(!isInInventory(newItem)){
        print("check");
        InventoryItems.Add(newItem);
        InventoryItemIDs.Add(newItem.ItemID);
        Dock.AddItem(InventoryItems);
        }


   }

   public void RemoveItem(int itemID){
       Item removeItem = FindItem(itemID);
       if(removeItem != null){ 
        InventoryItems.Remove(removeItem);
        InventoryItemIDs.Remove(itemID);
        }
   }


   public Item FindItem(int itemID){
       Item returnItem = null;
       foreach(Item item in InventoryItems){
           if(item.ItemID == itemID) return item;
       }
       return null;
   }



   private bool isInInventory(Item item){
       bool contained = false;
       foreach(Item item1 in InventoryItems){
           if(item1.ItemID == item.ItemID) return true;
       }
       return contained;
   }
}
