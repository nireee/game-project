using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPuzzle : MonoBehaviour
{
    public Transform ClockDial;
    public ClockHand Minute, Hour;

    public float x = 1;
    public float y = 1;

    public int MinuteSolution = 11;
    public int HourSolution = 6;

    //current value of the hands
    private int Minutes = -1;
    private int Hours = -1;

    public bool Completed = false;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHands();
        if(Minutes == MinuteSolution && Hours == HourSolution)
        {
            Completed = true;
        }
    }

    public bool CanTouch()
    {
        if (FindObjectOfType<TouchHandler>().CanTouch(gameObject) && !Completed) return true;
        else return false;
    }

    public void CheckHands()
    {

        //return the time base on hand positions

        Minutes = -1; //return correct minutes
        Hours = -1; //return correct hour
    }
    float GetAngle(float x, float y)
    {
        
        float f = Mathf.Abs(y / x);
        float theta = Mathf.Atan(f) * 180 / 3.14f;

        if (x < 0 && y >= 0) // quadrant II
        {
            theta = 180 - theta;
        }
        else if (x < 0 && y < 0) // quadrant III
        {
            theta += 180;
        }
        else if (x > 0 && y < 0) // quadrant IV
        {
            theta = 360 - theta;
        }

        return theta;
    }

    float GetAngle(Vector2 origin, Vector2 point)
    {
        return GetAngle(point.x - origin.x, point.y - origin.y);
    }

    public float GetAngle(Vector2 point)
    {
        return GetAngle(ClockDial.position, point);
    }
}
