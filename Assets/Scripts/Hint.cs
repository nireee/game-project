using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    [TextArea] public string MyHint = "Please write a note";
    

    private void OnMouseUp()
    {
        FindObjectOfType<DialogWindow>().DisplayDialog(this.MyHint);
    }
}
