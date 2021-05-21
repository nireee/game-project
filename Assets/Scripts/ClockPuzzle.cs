using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPuzzle : MonoBehaviour
{
    public Transform ClockDial;
    public ClockHand Minute, Hour;
    float x = -1;
    float y = 2;

    public int MinuteSolution = 40;
    public int HourSolution = 8;

    //current value of the hands
    public int Minutes = -1;
    public int Hours = -1;

    public bool Completed = false;

    public float HandDropRadius = 1;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHand();
        if (Minutes == MinuteSolution && Hours == HourSolution) Completed = true;
    }

    public bool CheckHandDropRadius(Vector2 point, Transform hand)
    {
        if (Vector2.Distance(point, ClockDial.position) < HandDropRadius)
        {
            hand.position = new Vector3(ClockDial.position.x, ClockDial.position.y, hand.position.z);
            return true;
        }
        else return false;
    }

    public void CheckHand()
    {
        //Vector2 Hour_vec = new Vector2(Hour.transform.position.x, Hour.transform.position.y);
        //if(h_angle <= 90)
        //{
        //    Hours = 3;
        //    int result = (int)h_angle / 30;
        //    Hours -= result;
        //}
        //else if(h_angle > 90 && h_angle <= 180)
        //{
        //    Hours = 12;
        //    int result = (int)h_angle / 30;
        //    Hours -= (3- result);
        //}
        //else if(h_angle > 180 && h_angle <= 270)
        //{
        //    Hours = 9;
        //    int result = (int)h_angle / 30;
        //    Hours -= result;
        //}
        //else if(h_angle >270 && h_angle <= 360)
        //{
        //    Hours = 6;
        //    int result = (int)h_angle / 30;
        //    Hours -= (3 - result);
        //}
        float h_angle = Hour.transform.eulerAngles.z;
        int h_inverse = (int)((360-h_angle) / 30);
        Hours = h_inverse + 3;
        Hours = Hours > 12 ? Hours - 12 : Hours;

        //Vector2 Minute_vec = new Vector2(Minute.transform.position.x, Minute.transform.position.y);
        float m_angle = Minute.transform.eulerAngles.z;
        int m_inverse = (int)((360 - m_angle) / 6);
        Minutes = m_inverse + 15;
        Minutes = Minutes > 59 ? Minutes - 60 : Minutes;
        //if (m_angle <= 90)
        //{
        //    Minutes = 15;
        //    int result = (int)m_angle / 6;
        //    Minutes -= result;
        //}
        //else if (m_angle > 90 && m_angle <= 180)
        //{
        //    Minutes = 60;
        //    int result = (int)m_angle / 6;
        //    Minutes -= (15 - result);
        //}
        //else if (m_angle > 180 && m_angle <= 270)
        //{
        //    Minutes = 45;
        //    int result = (int)m_angle / 6;
        //    Minutes -= result;
        //}
        //else if (m_angle > 270 && m_angle <= 360)
        //{
        //    Minutes = 30;
        //    int result = (int)m_angle / 6;
        //    Minutes -= (15 - result);
        //}



        //return the time based on hand positions
        // fill in this before next class

    }

    float GetAngle(float x, float y)
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
        else if (x == 0 && y < 0)
        {
            theta = 270;
        }
        else if (x > 0 && y < 0)
        {
            theta = 90 - theta;
            theta += 270;
        }
        return theta;
    } 

    float GetAngle(Vector2 origin, Vector2 point)
    {
        return GetAngle(point.x - origin.x,point.y - origin.y);
    }

    public float GetAngle(Vector2 point)
    {
        return GetAngle(ClockDial.position, point);
    }

    public bool CanTouch()
    {
        if (FindObjectOfType<TouchHandler>().CanTouch(gameObject) && !Completed) return true;
        else return false;
    }

}
