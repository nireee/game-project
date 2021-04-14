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
        if (isOpen) OpenInventroyDock();
        else CloseInventoryDock();
        if(Inventory.StaticInventory != null) inventory = Inventory.StaticInventory;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown(){
        if (!FindObjectOfType<TouchHandler>().CanTouch(gameObject)) return;
        if (isOpen){
            CloseInventoryDock();
        }
        else{
            OpenInventroyDock();
        }
        isOpen = !isOpen;
    }

    int currentDockIndex = 0;

    public void AddItem(Item InventoryItem){

        if (!isOpen)
        {
            InventoryItem.gameObject.SetActive(false);
        }
        else placeItemInDock(InventoryItem);
        InventoryItem.SetItemState(Item.ItemStates.found);

    }

    public void CloseInventoryDock()
    {
        //hide items
        Dock.SetActive(false);
        for (int i = 0; i < inventory.InventoryItems.Count; i += 1)
        {
            if(inventory.InventoryItems[i] && inventory.InventoryItems[i].GetComponent<Item>().GetItemStates() != Item.ItemStates.placed)
            {
                inventory.InventoryItems[i].gameObject.SetActive(false);
            }
        }

    }

    public void OpenInventroyDock()
    {
        //display items
        Dock.SetActive(true);
        for (int i = 0; i < inventory.InventoryItems.Count; i += 1)
        {
            if (inventory.InventoryItems[i] && inventory.InventoryItems[i].GetComponent<Item>().GetItemStates() != Item.ItemStates.hidden)
            {
                inventory.InventoryItems[i].gameObject.SetActive(true);
            }
        }

        currentDockIndex = 0;
        for (int i = 0; i < inventory.InventoryItems.Count; i += 1)
        {
            if (inventory.InventoryItems[i] && inventory.InventoryItems[i].GetComponent<Item>().GetItemStates() != Item.ItemStates.placed /*&& InventoryItems[i].GetComponent<Item>().ItemState != Item.ItemStates.hidden*/)
            {
                
                placeItemInDock(inventory.InventoryItems[i]);
            }

        }

    }

    private void placeItemInDock(Item item)
    {
        item.DockItem();
        Vector2 itemLoc = new Vector2(Dock.transform.position.x, Dock.transform.position.y + ItemStart + ItemSpacing * currentDockIndex);
        item.transform.position = itemLoc;
        currentDockIndex += 1;
    }


}
