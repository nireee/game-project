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
    private Vector3 MaximizeLoc = new Vector3(0, 0, foundZDepth);
    public float MaximiziedScale = 2;
    public float MaximizingRate = 0.25f;
    public float DockScale = 0.5f;
    public float DockingRate = 1;
    public float UndockingRate = 1;
    public float UndockScale = 1;

    public float MaximizedPause = 999;
    private float maximizedPauseStart;
    private bool maximizedCancel = false;
    private static float foundZDepth = -5;
    public Vector2 DefaultScale;
    private float TargetScale;
    private Vector3 TargetLoc;

    public static int OrderInLayer = 1;

    private bool preloaded = false;
    public SpriteRenderer AlternateSprite;

    private void Awake()
    {
        if(!preloaded) DefaultScale = transform.lossyScale;

        //handle spriterenderer not being on the parent object
        sprite = GetComponent<SpriteRenderer>();
        if (!sprite) sprite = AlternateSprite;

        OrderInLayer = Mathf.Max(sprite.sortingOrder, OrderInLayer);
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
                    IncrementSortingOrder();
                   AnimationState += 1;
                }
                
                break;
            case AnimationStates.maximizing:
                TargetScale = MaximiziedScale;
                TargetLoc = MaximizeLoc;
                float dScale = MaximiziedScale - transform.lossyScale.x / DefaultScale.x;
                float time = dScale / MaximizingRate;
                float speed = Vector2.Distance(TargetLoc, transform.position) / time;
                translateItem(speed);

                if (scaleItem(MaximizingRate))
                {
                    AnimationState += 1;
                    maximizedCancel = false;
                    maximizedPauseStart = Time.fixedTime;
                    FindObjectOfType<TouchHandler>().ClearCanTouchObjects(gameObject);
                }
                break;
            case AnimationStates.maximized:
                if(maximizedCancel == true || maximizedPauseStart + MaximizedPause < Time.fixedTime)
                {
                    AnimationState += 1;
                    //FindObjectOfType<TouchHandler>().ClearCanTouchObjects(null);
                }
                break;
            case AnimationStates.docking:
                AddItemToDock();
                AnimationState += 1;
                break;
            case AnimationStates.docked:
                
                break;
            case AnimationStates.undocking:
                TargetScale = UndockScale;
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
            transform.position = Vector3.MoveTowards(transform.position, TargetLoc, speed * Time.deltaTime);
            return false;
        }
    }

    private bool scaleItem(float rate)
    {
        rate *= DefaultScale.x;
        float currentScale = transform.localScale.x / DefaultScale.x;
        if (rate < 0 && TargetScale - currentScale > rate * Time.deltaTime || rate > 0 && TargetScale - currentScale < rate * Time.deltaTime)
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

    

    private void OnMouseDown()
        {
        //AudioHandler.playFX("jump");//play sound effect
        if (!FindObjectOfType<TouchHandler>().CanTouch(gameObject)) return;
        if(AnimationState == AnimationStates.maximizing ||  AnimationState == AnimationStates.maximized || AnimationState == AnimationStates.docking)
        {
            DockItem();
        }
        if (AnimationState == AnimationStates.hidden && ItemState == ItemStates.hidden)
        {
            itemFound();
        }

        if(ItemState != ItemStates.placed && AnimationState > AnimationStates.poppingOut)
        {
            IncrementSortingOrder();
        }
        
    }

    private SpriteRenderer sprite;
    public void IncrementSortingOrder()
    {

        if (sprite.sortingOrder <= OrderInLayer) OrderInLayer += 2;
        else OrderInLayer = sprite.sortingOrder + 2;
        transform.position = new Vector3(transform.position.x, transform.position.y, foundZDepth);
        sprite.sortingOrder = OrderInLayer;
    }

    //private void OnMouseUp(){
    //    if (!FindObjectOfType<TouchHandler>().CanTouch(gameObject)) return;
    //    if (ItemState == ItemStates.hidden)
    //    {
    //        print("OnMouseUp" + gameObject.name);
    //        SetItemState(ItemStates.found);
    //    }
    //}

    public void DockItem()
    {
        preloaded = true;
        AnimationState = AnimationStates.docked;
        transform.localScale = DockScale * DefaultScale;
        if (!FindObjectOfType<Inventory>().isInInventory(this))
        {
            AddItemToDock();
        }
    }


    private void itemFound(){
        transform.parent = null;

        FindObjectOfType<TouchHandler>().ClearCanTouchObjects(null);
        TargetLoc = (Vector2)transform.position + PopOutTranslation;
        AnimationState = AnimationStates.poppingOut;
    // animation that make the item rise up and surrounding by lights and play sounds
        //FindObjectOfType<Inventory>().AddItem(this);
       
    //    gameObject.SetActive(false);
    }

    private void AddItemToDock()
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
