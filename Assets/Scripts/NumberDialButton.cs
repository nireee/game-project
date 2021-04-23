using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberDialButton : MonoBehaviour
{
    public NumberDial NumberDial;
    public bool DialUp;
    public NumberLock NumLock;
    public bool ComboCompleted;

    private void OnMouseDown()
    {
        CheckCombo();
        if(ComboCompleted != true)
        {
            if (DialUp) NumberDial.MoveDialUp();
            else NumberDial.MoveDialDown();
        }

    }
    private void CheckCombo() {
        //    if (NumberDial.TargetNumber == NumberDial.DisplayedNum) Completed = true;
        //    else Completed = false;
        if (NumLock.Completed == true) ComboCompleted = true;
        else ComboCompleted = false;
    }
    


}
