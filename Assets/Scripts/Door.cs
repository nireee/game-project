using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Vector2 DropArea = new Vector2(1, 2);
    

    public bool KeyDropped(Key key)
    {
        float KeyX = key.transform.position.x;
        float KeyY = key.transform.position.y;
        float DoorX = transform.position.x;
        float DoorY = transform.position.y;
        if(KeyX > DoorX - DropArea.x/2 && KeyX < DropArea.x/2 + DoorX && KeyY > DoorY - DropArea.y / 2 && KeyY < DropArea.y / 2 + DoorY)
        {
            FindObjectOfType<Portal>().Active = true;
            return true;
        }
        else{
            return false;
        }
    }
}
