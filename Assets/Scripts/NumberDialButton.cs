using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberDialButton : MonoBehaviour
{
    public NumberDial NumberDial;
    public bool DialUp;

    private void OnMouseDown()
    {
        print("Dial mouseDown");
        if (DialUp) NumberDial.MoveDialUp();
        else NumberDial.MoveDialDown();
    }
}
