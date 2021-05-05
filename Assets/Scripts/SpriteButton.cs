using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteButton : MonoBehaviour
{
    public GameObject TriggerObj;

    private void OnMouseDown()
    {
        TriggerObj.SendMessage("SpriteButton");
    }

    


}
