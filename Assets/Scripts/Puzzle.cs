using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : Zoomable
{
    public PuzzlePiece[] Pieces;
    public bool Finished;
    public Key key;


    void Update(){
        if (!Finished)
        {
            Finished = CheckedFinished();
            if (Finished)
            {
                key.gameObject.SetActive(true);
                key.gameObject.GetComponent<Item>().IncrementSortingOrder();
            }
            
        }
        ParentUpdate();
    }

    public bool CheckedFinished(){
        bool finished = true;
        foreach(PuzzlePiece piece in Pieces){
            if(piece.gameObject.GetComponent<Item>().GetItemStates() != Item.ItemStates.placed) finished = false;
        }
        return finished;
    }
    
    
}
