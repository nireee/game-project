using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    protected Vector2 displacement;
    public bool Dragable = true;

    private void OnMouseDown()
    {
        if(Dragable)
            displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        if (Dragable)
        {
            print("hello");
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //print(Input.mousePosition + " -> " + mousePos);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z) + (Vector3)displacement;
        }
    }
}
