using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : DragableObject
{
    [SerializeField]private int PieceId = 0;
    public Neighbor[] Neighbors;
    
    
    [System.Serializable]public struct Neighbor{
        public PuzzlePiece Piece;
        public Vector2 Offset;
    }

    public float SnapThreshold = 0.1f;
    public static int OrderInLayer = 1;


    private Item itemScript;


    // Start is called before the first frame update
    void Start()
    {
        itemScript = GetComponent<Item>();
        Dragable = false;
        if(transform.parent && transform.parent.GetComponent<PuzzlePiece>()){
           Neighbor neighbor = new Neighbor();
           neighbor.Piece = transform.parent.GetComponent<PuzzlePiece>();
           neighbor.Offset = neighbor.Piece.transform.position - transform.position;
           print(gameObject.name + " " + neighbor.Offset.x + "," + neighbor.Offset.y);
        //    Neighbors = new Neighbor[1];
        //    Neighbors[0] = neighbor;
            transform.parent = transform.parent.parent;
        }
    }
    public void OnMouseDown(){
        if (!FindObjectOfType<TouchHandler>().CanTouch(gameObject)) return;
        if (itemScript.GetItemStates() != Item.ItemStates.hidden && itemScript.GetItemStates() != Item.ItemStates.placed)
       { 
           Dragable = true;
           displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(transform.parent){
            if(transform.parent.GetComponent<PuzzlePiece>()){
                ParentChildSwapping(transform.GetComponent<PuzzlePiece>(), transform.parent.GetComponent<PuzzlePiece>());
            }
            
        }
        OrderInLayer += 1;
        GetComponent<SpriteRenderer>().sortingOrder = OrderInLayer;
        }
        else
        {
        Dragable = false;
        }
    }
    private static void ParentChildSwapping(PuzzlePiece parent, PuzzlePiece child){
        PuzzlePiece[] pieces = child.GetComponentsInChildren<PuzzlePiece>();
        //Transform parent = transform parent;
        foreach(PuzzlePiece piece in pieces){
            if(piece.transform != parent.transform) piece.transform.parent = parent.transform;
            }
        if(child.transform == parent.transform.parent){
            parent.transform.parent = child.transform.parent;
        }
        child.transform.parent = parent.transform;
        //parent.parent = transform;

    }

    private bool checkPiecePossition(Neighbor piece){
        return Vector2.Distance(transform.position, (Vector2)piece.Piece.transform.position - piece.Offset) < SnapThreshold;
    }

    private void OnMouseUp(){
        if (!FindObjectOfType<TouchHandler>().CanTouch(gameObject)) return;
        if (!Dragable) return;
        if(itemScript.GetItemStates() != Item.ItemStates.hidden && itemScript.GetItemStates() != Item.ItemStates.placed)
        {
            if(!Dragable) return;
            foreach (Neighbor neighbor in Neighbors)
            {
            if (checkPiecePossition(neighbor)){
                if(neighbor.Piece.Dragable){
                    neighbor.Piece.transform.position = (Vector2)transform.position + neighbor.Offset;
                    ParentChildSwapping(transform.GetComponent<PuzzlePiece>(), neighbor.Piece);
                }else{
                    itemScript.SetItemState(Item.ItemStates.placed);
                        FindObjectOfType<Inventory>().PlaceItem(itemScript);
                    Dragable = false;
                    transform.position = (Vector2)neighbor.Piece.transform.position - neighbor.Offset;
                    ParentChildSwapping(neighbor.Piece, transform.GetComponent<PuzzlePiece>());
                }
            }
            }
        }
        
    }

    // public void OnMouseDrag(){
    //     Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z) + (Vector3)displacement;
    // }

    // private void CorrectedPlaced(){
    //     if(piece1.transform.position == new Vector3(-0.28f, 3.21f, 2.602129f)) Dragable = false;
    //     if(piece2.transform.position == new Vector3(1.653383f, 3.182652f, 2.602129f)) Dragable = false;
    // }


    
    
};
