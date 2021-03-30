using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public PuzzlePiece[] Pieces;
    public bool Finished;

    void Update(){
        Finished = CheckedFinished();
    }

    public bool CheckedFinished(){
        bool finished = true;
        foreach(PuzzlePiece piece in Pieces){
            if(piece.Dragable) finished = false;
        }
        return finished;
    }
    
    
}
