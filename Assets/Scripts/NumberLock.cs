using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberLock : MonoBehaviour
{
    public NumberDial Dial1, Dial2, Dial3;
    public bool Completed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckCombo();
    }
    void CheckCombo()
    {
        
        if(Dial1.TargetNumber == Dial1.DisplayedNum && Dial2.TargetNumber == Dial2.DisplayedNum && Dial3.TargetNumber == Dial3.DisplayedNum)
        {
            Completed = true;
        }
        else
        {
            Completed = false;
        }
    }
}
