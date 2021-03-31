using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
   public GameObject ItemPrefab;
   public int ItemID = -1;

   public enum ItemStates{
       hidden, 
       found,
       placing,
       placed
   }
   [SerializeField] private ItemStates ItemState = ItemStates.hidden;


   private void OnMouseDown(){
       
        if(ItemState == ItemStates.hidden){
            itemFound();
            
        }
   }

    private void OnMouseUp()
    {
        if (ItemState == ItemStates.hidden)
        {
            SetItemState(ItemStates.found);
        }
    }

    private void itemFound(){
       // animation that make the item rise up and surrounding by lights and play sounds
       FindObjectOfType<Inventory>().AddItem(this);
       
    //    gameObject.SetActive(false);
   }

    public void SetItemState(ItemStates state)
    {
        ItemState = state;
    }
    public ItemStates GetItemState()
    {
        return ItemState;
    }
}
