using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : DragableObject
{
    private void OnMouseUp()
    {
        if (FindObjectOfType<Door>().KeyDropped(this))
        {
            GetComponent<Item>().SetItemState(Item.ItemStates.placed);
            FindObjectOfType<Inventory>().PlaceItem(GetComponent<Item>());
            gameObject.SetActive(false);
        }
    }
}
