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
            Dock.SetActive(false);
        }
        else{
            Dock.SetActive(true);
        }
        isOpen = !isOpen;
    }

    public void AddItem(List<Item> InventoryItems){
        for(int i = 0; i < InventoryItems.Count; i += 1){
            Vector2 itemLoc = new Vector2(Dock.transform.position.x, Dock.transform.position.y + ItemStart + ItemSpacing * i);
            InventoryItems[i].transform.position = itemLoc;
        }
    }
}
