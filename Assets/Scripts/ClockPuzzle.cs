using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPuzzle : Zoomable
{
    public Transform ClockDial;
    public ClockHand Minute, Hour;
    public Portal TV;
    public GameObject TV_on;

    float x = -1;
    float y = 2;

    public int MinuteSolution = 40;
    public int HourSolution = 8;

    //current value of the hands
    public int Minutes = -1;
    public int Hours = -1;

    //public bool Completed = false;

    public float HandDropRadius = 1;
    // Start is called before the first frame update
    void Start()
    {
        ParentStart();
        TV_on.gameObject.SetActive(false);
        //ZoomCollider.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        ParentUpdate();
        CheckHand();
        if (Minutes == MinuteSolution && Hours == HourSolution && !Completed)
        {
            Completed = true;
            TV.Active = true;
            TV.GetComponent<SpriteRenderer>().enabled = false;
            TV_on.gameObject.SetActive(true);
        }
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

        float h_angle = Hour.transform.eulerAngles.z;


        int h_inverse = (int)((360 - h_angle) / 30);
        Hours = h_inverse + 3;
        Hours = Hours > 12 ? Hours - 12 : Hours;


        float m_angle = Minute.transform.eulerAngles.z;
        int m_inverse = (int)((360 - m_angle) / 6);
        Minutes = m_inverse + 15;
        Minutes = Minutes > 59 ? Minutes - 60 : Minutes;



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
        return GetAngle(point.x - origin.x, point.y - origin.y);
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

    protected override bool completedCondition()
    {
        return Completed;
    }

    protected override void completedOpen()
    {
        //open DrawerInside


    }



    protected override void closeZoomable()
    {
        transform.localScale = DefaultScale;

    }

    protected override bool openPuzzleCheck()
    {
        return Hour.isPlaced && Minute.isPlaced;
    }

    //public void HandPlaced()
    //{
    //    if (Hour.isPlaced && Minute.isPlaced) ZoomCollider.enabled = true;
    //}
}