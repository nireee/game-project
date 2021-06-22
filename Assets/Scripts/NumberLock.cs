using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberLock : Zoomable
{
    public NumberDial Dial1, Dial2, Dial3;
    public int num1, num2, num3;

    //boolean



    public GameObject DrawerInside;
    public GameObject LockMesh;


    // Start is called before the first frame update
    void Start()
    {
        ParentStart();
        if (transform.parent) transform.parent = null;
        DrawerInside.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        CheckCombo();

        ParentUpdate();
    }
    void CheckCombo()
    {

        if (Dial1.TargetNumber == num1 && Dial2.TargetNumber == num2 && Dial3.TargetNumber == num3)
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
        return ParentCanTouch();
    }

    protected override bool completedCondition()
    {
        return Completed && !DrawerInside.activeSelf;
    }

    protected override void completedOpen()
    {
        //open DrawerInside
        CancelButton.gameObject.SetActive(true);
        DrawerInside.SetActive(true);

        LockMesh.SetActive(false);

    }



    protected override void closeZoomable()
    {
        transform.localScale = DefaultScale;
        DrawerInside.SetActive(false);
        LockMesh.SetActive(true);
    }


}