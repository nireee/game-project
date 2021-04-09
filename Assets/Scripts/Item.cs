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
        poppingOut, //state 1
        maximizing, //state 2
        maximized,
        docking, //state 3
        docked,
        undocking, //state 4
        undocked
    }
    public AnimationStates AnimationState;
    
    public Vector2 PopOutTranslation = new Vector2(2, 0);
    public float PopOutTranslationSpeed = 0.5f; //units per second
    public Vector2 MaximizedLoc = new Vector2(0, 0);
    public float MazimizingRate = 1;
    public float MaximizedScale = 2;
    public float DockedScale = 0.5f;
    public float DockingRate = 1;
    public float UndockingRate = 1;

    private Vector2 DefaultScale;
    private float TargetScale;
    private Vector2 TargetLoc;

    public float MaximizedPause = 1;
    private float maximizedPauseStart;
    private bool maximizedCancel = false;

    public static int OrderInLayer = 1;

    private void Start()
    {
        DefaultScale = transform.lossyScale;
    }
    private void Update()
    {
        switch (AnimationState)
        {
            case AnimationStates.hidden:
                //transitions to poppingOut in itemFound Method
                break;
            case AnimationStates.poppingOut:
                if (translateItem(PopOutTranslationSpeed)){
                    AnimationState += 1;
                }
                break;
            case AnimationStates.maximizing:
                TargetScale = MaximizedScale;
                TargetLoc = MaximizedLoc;

                float dScale = MaximizedScale - transform.lossyScale.x / DefaultScale.x;
                float time = dScale / MazimizingRate;
                float speed = Vector2.Distance(TargetLoc, transform.position) / time;
                translateItem(speed);
                print(transform.position);
                 print(speed + " " + dScale + " " + time);

                if (scaleItem(MazimizingRate))
                {
                    AnimationState += 1;
                    maximizedCancel = false;
                    maximizedPauseStart = Time.fixedTime;
                    FindObjectOfType<TouchHandler>().ClearCanTouchObjects(gameObject);
                }
                    
                
                break;
            case AnimationStates.maximized:
                if(maximizedCancel || maximizedPauseStart + MaximizedPause < Time.fixedTime) {
                    AnimationState += 1;
                    //FindObjectOfType<TouchHandler>().ClearCanTouchObjects(null);
                }
                
                break;
            case AnimationStates.docking:
                addItemToDock();
                AnimationState += 1;
                break;
            case AnimationStates.docked:

                break;
            case AnimationStates.undocking:
                TargetScale = 1;
                if (scaleItem(UndockingRate))
                {
                    AnimationState += 1;
                }
                break;
            case AnimationStates.undocked:

                break;
            default:
                print("Added state not handled");
                break;
        }
    }

    private bool translateItem(float speed)
    {
        if (Vector2.Distance(TargetLoc, transform.position) < speed * Time.deltaTime)
        {
            transform.position = TargetLoc;
            print("set position");
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
        float currentScale = transform.lossyScale.x / DefaultScale.x;
        float deltaScale = TargetScale - currentScale;

        if (rate < 0 && deltaScale > rate * Time.deltaTime || rate > 0 && deltaScale < rate * Time.deltaTime)
        {
            transform.localScale = TargetScale * DefaultScale;
            return true;
        }
        else
        {
            float yScale = DefaultScale.y / DefaultScale.x;
            transform.localScale += rate * Time.deltaTime * new Vector3(1, yScale, 1); 
            return false;
        }
    }

    private void OnMouseDown(){
        if (!FindObjectOfType<TouchHandler>().CanTouch(gameObject)) return;

        if (AnimationState == AnimationStates.maximizing ||  AnimationState == AnimationStates.maximized || AnimationState == AnimationStates.docking)
        {
            DockItem();
        }
        else if (ItemState == ItemStates.hidden && AnimationState == AnimationStates.hidden)
        {
            itemFound();

        }

        if(ItemState != ItemStates.placed)
        {
            OrderInLayer += 1;
            GetComponent<SpriteRenderer>().sortingOrder = OrderInLayer;
        }
         
   }

    public void DockItem()
    {
        AnimationState = AnimationStates.docked;
        transform.localScale = DockedScale * DefaultScale;
        if(!FindObjectOfType<Inventory>().isInInventory(this)) addItemToDock();
    }

    //private void OnMouseUp()
    //{
    //    if (!FindObjectOfType<TouchHandler>().CanTouch(gameObject)) return;
    //    if (ItemState == ItemStates.hidden)
    //    {
    //        print("OnMouseUp " + gameObject.name);
    //        SetItemState(ItemStates.found);
    //    }
    //}

    private void itemFound(){
        FindObjectOfType<TouchHandler>().ClearCanTouchObjects(null);

        // animation that make the item rise up and surrounding by lights and play sounds
        TargetLoc = (Vector2)transform.position + PopOutTranslation;
        AnimationState = AnimationStates.poppingOut;

   }
    private void addItemToDock()
    {
        FindObjectOfType<Inventory>().AddItem(this);
        FindObjectOfType<TouchHandler>().ClearCanTouchObjects();
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
