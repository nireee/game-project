using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject ItemPrefab;

    public enum ItemStates
    {
        hidden,
        found,
        placing,
        placed
    }
    public ItemStates ItemState = ItemStates.hidden;

    private void OnMouseDown()
    {
        if(ItemState == ItemStates.hidden)
        {
            itemFound();
        }
    }

    private void itemFound()
    {
        FindObjectOfType<Inventory>().AddItem(this);
        gameObject.SetActive(false);
    }

}
