using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public PuzzlePiece[] Pieces;
    public bool Finished;

    // Update is called once per frame
    void Update()
    {
        if(!Finished) Finished = CheckFinished();
    }

    public bool CheckFinished()
    {
        bool finished = true;
        foreach(PuzzlePiece piece in Pieces)
        {
            if (piece.Dragable) finished = false;
        }

        return finished;
    }
}
