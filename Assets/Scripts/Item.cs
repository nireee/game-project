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
    public enum AnimationStates
    {
        hidden,
        poppingOut, // state 1
        maximizing, // state 2
        maximized,
        docking,    // state 3
        docked,
        undocking,  // state 4
        undocked
    }

    public AnimationStates AnimationState;
    public Vector2 PopOutTranslation = new Vector2(2, 0);
    public float PopOutTranslationSpeed = 0.5f; // units per second
    private Vector2 MaximizeLoc = new Vector2(0, 0);
    public float MaximiziedScale = 2;
    public float MaximizingRate = 1;
    public float DockScale = 0.5f;
    public float DockingRate = 1;
    //public float UndockingSpeed = 1;

    private Vector2 DefaultScale;
    private float TargetScale;
    private Vector2 TargetLoc;

    private void Start()
    {
        DefaultScale = transform.localScale;
    }

    private void Update()
    {
        switch (AnimationState)
        {
            case AnimationStates.hidden:
                //translations to poppingOut in itemFound method;
                break;
            case AnimationStates.poppingOut:
                if (translateItem(PopOutTranslationSpeed))
                {
                   AnimationState += 1;
                }
                
                break;
            case AnimationStates.maximizing:
                //if(scaleItem(MaximizingRate))
                AnimationState += 1;
                break;
            case AnimationStates.maximized:
                AnimationState += 1;
                break;
            case AnimationStates.docking:
                AddItemDock();
                AnimationState += 1;
                break;
            case AnimationStates.docked:
                
                break;
            case AnimationStates.undocking:
                
                break;
            case AnimationStates.undocked:
                
                break;
            default:
                print("Added state not handled");
                break;
        }
    }

    public bool translateItem(float speed)
    {
        if (Vector2.Distance(TargetLoc, transform.position) < speed * Time.deltaTime)
        {
            transform.position = TargetLoc;
            //AnimationState = AnimationStates.maximizing;
            return true;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, TargetLoc, speed * Time.deltaTime);
            return false;
        }
    }

    private bool scaleItem(float rate)
    {
        float currentScale = transform.localScale.x / DefaultScale.x;
        if(rate < 0 && TargetScale + currentScale < rate * Time.deltaTime || rate > 0 && TargetScale - currentScale > rate * Time.deltaTime)
        {
            transform.localScale = TargetScale * DefaultScale;
            return true;
        }
        else
        {
            transform.localScale += rate * Time.deltaTime * new Vector3(1, 1, 1);
            return false;
        }
    }

    private void OnMouseDown()
    {
        if (!FindObjectOfType<TouchHandler>().CanTouch(gameObject)) return;
        if (ItemState == ItemStates.hidden)
        {
            itemFound();
        }
    }

    //private void OnMouseUp(){
    //    if (!FindObjectOfType<TouchHandler>().CanTouch(gameObject)) return;
    //    if (ItemState == ItemStates.hidden)
    //    {
    //        print("OnMouseUp" + gameObject.name);
    //        SetItemState(ItemStates.found);
    //    }
    //}



    private void itemFound(){

        FindObjectOfType<TouchHandler>().ClearCanTouchObjects(null);
        TargetLoc = (Vector2)transform.position + PopOutTranslation;
        AnimationState = AnimationStates.poppingOut;
    // animation that make the item rise up and surrounding by lights and play sounds
        //FindObjectOfType<Inventory>().AddItem(this);
       
    //    gameObject.SetActive(false);
    }

    private void AddItemDock()
    {
        FindObjectOfType<Inventory>().AddItem(this);
        FindObjectOfType<TouchHandler>().ClearCanTouchObjects();
    }

    public ItemStates GetItemStates()
    {
        return ItemState;
    }


    public void SetItemState(ItemStates state)
    {
        ItemState = state;
    }
}
