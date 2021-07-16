using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : DragableObject
{
    void Start()
    {
        itemScript = GetComponent<Item>();
    }
    Item itemScript;
    private void OnMouseUp()
    {
        if (FindObjectOfType<Door>().KeyDropped(this))
        {
            GetComponent<Item>().SetItemState(Item.ItemStates.placed);
            FindObjectOfType<Inventory>().PlaceItem(GetComponent<Item>());
            gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (!FindObjectOfType<TouchHandler>().CanTouch(gameObject)) return;
        if (itemScript.GetItemStates() != Item.ItemStates.hidden && itemScript.GetItemStates() != Item.ItemStates.placed)
        {
            Dragable = true;
            displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (itemScript.AnimationState == Item.AnimationStates.docked) itemScript.AnimationState = Item.AnimationStates.undocking;
        }
        else
        {
            Dragable = false;
        }
    }
}
