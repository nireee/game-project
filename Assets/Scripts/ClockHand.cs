using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHand : MonoBehaviour
{
    public ClockPuzzle CP;
    private Item itemScript;
    // Start is called before the first frame update
    void Start()
    {
        itemScript = GetComponent<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        
    }

    private void OnMouseDrag()
    {
        if (CP.CanTouch())
        {
            if(itemScript.AnimationState == Item.AnimationStates.undocked)
            {
                float angle = CP.GetAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
            
        }
        
    }
}
