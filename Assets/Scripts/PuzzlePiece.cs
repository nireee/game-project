using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private int PieceId = 0;
    Vector2 displacement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        print("hello");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        print(Input.mousePosition + " -> " + mousePos);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z) + (Vector3) displacement;
    }

}
