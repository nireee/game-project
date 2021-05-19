using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPuzzle : MonoBehaviour
{
    public Transform ClockDial;
    public ClockHand Minute, Hour;
    float x = -1;
    float y = 2;
    // Start is called before the first frame update
    void Start()
    {
        
        

        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHand();
    }

    public void CheckHand()
    {
        float f = Mathf.Abs(y / x);
        float theta = Mathf.Atan(f) * 180 / 3.14f;

        if (x < 0 && y > 0)
        {
            theta = 90 - theta;
            theta += 90;
        }
        else if (x < 0 && y == 0)
        {
            theta = 180;
        }
        else if (x < 0 && y < 0)
        {
            theta += 180;
        }
        else if(x == 0 && y < 0)
        {
            theta = 270;
        }
        else if (x > 0 && y < 0)
        {
            theta += 270;
        }
        print(theta);

        // find out the correct solution
    }
}
