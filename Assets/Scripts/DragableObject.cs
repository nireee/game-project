using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    protected Vector2 displacement;
    public bool Dragable = true;

    private void OnMouseDown(){
        print("OnMouseDown");
        if (Dragable){
            displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }


    private void OnMouseDrag(){
        if(Dragable){
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z) + (Vector3)displacement;
        }
    }
}
