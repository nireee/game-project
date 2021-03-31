using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDock : MonoBehaviour
{
    public bool isOpen = false;
    public GameObject Dock;
    public Inventory inventory;
    public float ItemSpacing = 1.1f;
    public float ItemStart = 0.1f;
    public int ItemCount;

    // Start is called before the first frame update
    void Start()
    {
        if(Inventory.StaticInventory != null) inventory = Inventory.StaticInventory;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown(){
        if(isOpen){
            CloseInventoryDock();
        }
        else{
            OpenInventoryDock();
        }
        isOpen = !isOpen;
    }

    int currentDockIndex = 0;

    public void AddItem(Item InventoryItem)
    {
        if (!isOpen)
        {
            InventoryItem.SetItemState( Item.ItemStates.found);
            InventoryItem.gameObject.SetActive(false);
        }
        else placeItemInDock(InventoryItem);
    }

    public void CloseInventoryDock()
    {
        //hide items
        Dock.SetActive(false);
        for (int i = 0; i < inventory.InventoryItems.Count; i += 1)
        {
            if(inventory.InventoryItems[i].GetComponent<Item>().GetItemState() != Item.ItemStates.placed)
            {
                inventory.InventoryItems[i].gameObject.SetActive(false);
            }
        }
    }

    public void OpenInventoryDock()
    {
        //display items
        Dock.SetActive(true);
        for (int i = 0; i < inventory.InventoryItems.Count; i += 1)
        {
            if (inventory.InventoryItems[i].GetComponent<Item>().GetItemState() != Item.ItemStates.hidden)
            {
                inventory.InventoryItems[i].gameObject.SetActive(true);
            }
        }

        currentDockIndex = 0;
        //check if dock is open or closed
        for (int i = 0; i < inventory.InventoryItems.Count; i += 1)
        {
            if (inventory.InventoryItems[i].GetComponent<Item>().GetItemState() != Item.ItemStates.placed /*&& InventoryItems[i].GetComponent<Item>().ItemState != Item.ItemStates.hidden*/)
            {
                placeItemInDock(inventory.InventoryItems[i]);
            }

        }
    }

    private void placeItemInDock(Item item)
    {
        Vector2 itemLoc = new Vector2(Dock.transform.position.x, Dock.transform.position.y + ItemStart + ItemSpacing * currentDockIndex);
        item.transform.position = itemLoc;
        currentDockIndex += 1;
    }
}
