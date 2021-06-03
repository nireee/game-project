using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour
{
    public GameObject DialogPanel;
    public Text DialogText;

    public float DisplayTime = 5;
    private float DisplayStart = float.NegativeInfinity;
    // Start is called before the first frame update
    void Start()
    {
        DialogPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(DisplayTime + DisplayStart > Time.fixedTime)
        {
            DialogPanel.SetActive(true);
        }
        else
        {
            DialogPanel.SetActive(false);
        }
    }

    public void DisplayDialog(string message)
    {
        DialogText.text = message;
        DisplayStart = Time.fixedTime;
    }

    public void SpriteButton()
    {
        DisplayStart = float.NegativeInfinity;
    }

}
