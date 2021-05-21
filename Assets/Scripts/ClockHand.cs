using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHand : DragableObject
{
    public ClockPuzzle CP;
    private Item itemScript;

    public bool isPlaced = false;
    // Start is called before the first frame update
    void Start()
    {
        itemScript = GetComponent<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (itemScript.GetItemStates() != Item.ItemStates.hidden && itemScript.GetItemStates() != Item.ItemStates.placed)
        {
            if (itemScript.AnimationState == Item.AnimationStates.docked) itemScript.AnimationState = Item.AnimationStates.undocking;
            Dragable = true;
            displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            itemScript.IncrementSortingOrder();
        }
        else
        {
            Dragable = false;
        }
        
    }

    private void OnMouseDrag()
    {
        if (CP.CanTouch())
        {
            if (itemScript.GetItemStates() == Item.ItemStates.placed)
            {
                float angle = CP.GetAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
            else
            {
                OnMouseDragOverride();
            }

           
        }
    }

    private void OnMouseUp()
    {
        if(itemScript.AnimationState == Item.AnimationStates.undocked && CP.CheckHandDropRadius(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform))
        {
            itemScript.SetItemState(Item.ItemStates.placed);
            transform.parent = CP.transform;
            isPlaced = true;
            FindObjectOfType<Inventory>().PlaceItem(itemScript);
        }
    }
}
