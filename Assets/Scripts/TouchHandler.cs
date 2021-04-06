using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
    public bool CanTouchAll = true;
    public List<GameObject> CanTouchObjects;
    public bool CanTouch(GameObject touchedObject)
    {
        bool canTouch = false;
        if (CanTouchAll) canTouch = true;
        else
        {
            foreach(GameObject obj in CanTouchObjects)
            {
                if (touchedObject == obj) canTouch = true;
            }         
        }
        return canTouch;
    }
    public void SetCanTouchAll(bool canTouch)
    {
        CanTouchAll = canTouch;
    }
    /// <summary>
    /// Clear all touchObjects
    /// </summary>
    public void ClearCanTouchObjects()
    {
        CanTouchObjects.Clear();
        SetCanTouchAll(true);
    }
    /// <summary>
    /// Clear all touchObjects and add one touchObject
    /// </summary>
    /// <param name="touchObject"></param>
    public void ClearCanTouchObjects(GameObject touchObject)
    {
        CanTouchObjects.Clear();
        AddCanTouchObject(touchObject);
    }
    public void AddCanTouchObject(GameObject touchObject)
    {
        SetCanTouchAll(false);
        if (!CanTouchObjects.Contains(touchObject)) CanTouchObjects.Add(touchObject);
    }
}
