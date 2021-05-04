using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberLock : MonoBehaviour
{
    public NumberDial Dial1, Dial2, Dial3;
    public int num1, num2, num3;

    //boolean
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
        CheckCombo();

        if(!Expanded && expanding)
        {
            TargetScale = ExpandedScale;
            Expanded = scaleItem(ExpansionRate);
            if (Expanded) GetComponent<BoxCollider2D>().enabled = false;
        }
        else if(Expanded && !expanding)
        {
            TargetScale = 1;
            Expanded = !scaleItem(-ExpansionRate);
            if (!Expanded) GetComponent<BoxCollider2D>().enabled = true;

        }
    }
    void CheckCombo()
    {
        
        if(Dial1.TargetNumber == num1 && Dial2.TargetNumber == num2 && Dial3.TargetNumber == num3)
        {
            Completed = true;
        }
        else
        {
            Completed = false;
        }
    }
    public bool CanTouch()
    {
        if (FindObjectOfType<TouchHandler>().CanTouch(gameObject) && Expanded && !Completed && Expanded && expanding) return true;
        else return false;
    }

    private bool scaleItem(float rate)
    {
        rate *= DefaultScale.x;
        float currentScale = transform.localScale.x / DefaultScale.x;
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
            if (!Expanded && !expanding) expanding = true;
            else if(Expanded && expanding && Completed)
            {
                //open drawer
                
            }
        }
    }
}
