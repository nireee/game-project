using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : DragableObject
{
    [SerializeField] private int PieceId = 0;
    public Neighbor[] Neighbors;

    [System.Serializable]
    public struct Neighbor
    {
        public PuzzlePiece Piece;
        public Vector2 Offset;
    }

    public static float SnapThreshold = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        print("touched");
        displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (transform.parent)
        {
            if (transform.parent.GetComponent<PuzzlePiece>())
            {
                ParentChildSwapping(transform.GetComponent<PuzzlePiece>(), transform.parent.GetComponent<PuzzlePiece>());
            }
        }
    }

    public static void ParentChildSwapping(PuzzlePiece parent, PuzzlePiece child) 
    {
        PuzzlePiece[] pieces = child.GetComponentsInChildren<PuzzlePiece>();
        //Transform parent = transform.parent;
        foreach (PuzzlePiece piece in pieces)
        {
            if (piece.transform != parent.transform) piece.transform.parent = parent.transform;
        }

        if(child.transform == parent.transform.parent)
        {
            parent.transform.parent = child.transform.parent;
        }

        child.transform.parent = parent.transform;
        //parent.parent = transform;
    }

    private void OnMouseUp()
    {
        if (!Dragable) return;

        foreach(Neighbor neighbor in Neighbors)
        {
            if (checkPiecePossition(neighbor))
            {
                print("Next to neighbor");
                if (neighbor.Piece.Dragable)
                {
                    neighbor.Piece.transform.position = (Vector2)transform.position + neighbor.Offset;
                    ParentChildSwapping(transform.GetComponent<PuzzlePiece>(), neighbor.Piece);
                }
                else
                {
                    Dragable = false;
                    transform.position = (Vector2)neighbor.Piece.transform.position - neighbor.Offset;
                    ParentChildSwapping(neighbor.Piece, transform.GetComponent<PuzzlePiece>());
                }
                
            }
        }
    }

    private bool checkPiecePossition(Neighbor piece)
    {
        return Vector2.Distance(transform.position, (Vector2)piece.Piece.transform.position - piece.Offset) < SnapThreshold;
    }


}
