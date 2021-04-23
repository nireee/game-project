using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberLock : MonoBehaviour
{
    public NumberDial Dial1, Dial2, Dial3;
    public int Numb1, Numb2, Numb3;
    public bool Completed;
    public bool Expanded;
    public bool expanding = false;
    private bool open;

    private Vector2 DefaultScale;
    private float TargetScale;
    public float ExpandedScale = 4;
    public float ExpansionRate = 1;

    // Start is called before the first frame update
    void Start()
    {
        DefaultScale = transform.lossyScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Dial1.TargetNumber == Numb1 && Dial2.TargetNumber == Numb2 && Dial3.TargetNumber == Numb3) Completed = true;
        else
        {
            Completed = false;
        }

        if(!Expanded && expanding)
        {
            TargetScale = ExpandedScale;
            Expanded = scaleItem(ExpansionRate);
        }else if(Expanded && !expanding)
        {
            TargetScale = 1;
            Expanded = !scaleItem(-ExpansionRate);
        }

    }

    public bool CanTouch()
    {
        if (FindObjectOfType<TouchHandler>().CanTouch(gameObject) && Expanded && expanding && !Completed)
        {

            return true;
        }
        else return false;
    }

    private bool scaleItem(float rate)
    {
        float currentScale = transform.localScale.x / DefaultScale.x;

        rate *= DefaultScale.x;
        if (rate < 0 && TargetScale - currentScale > rate * Time.deltaTime || rate > 0 && TargetScale - currentScale < rate * Time.deltaTime)
        {
            transform.localScale = TargetScale * DefaultScale;
            return true;
        }
        else
        {
            float yScale = DefaultScale.y / DefaultScale.x;
            transform.localScale += rate * Time.deltaTime * new Vector3(1, yScale, 1);
            return false;
        }
    }

    private void OnMouseDown()
    {
        if (FindObjectOfType<TouchHandler>().CanTouch(gameObject))
        {
            if (!Expanded && !expanding)
            {
                expanding = true;
            }else if(Expanded && expanding && Completed)
            {
                //open drawer
            }
        }
    }
}
