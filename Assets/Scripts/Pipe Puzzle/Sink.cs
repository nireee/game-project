using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{

    public PipePuzzle pp;
    public GameObject EmptySink, FullSink;
    public SpriteButton cancel;

    void Start()
    {
        EmptySink.SetActive(false);
        FullSink.SetActive(false);
        cancel.gameObject.SetActive(false);
    }

    public void SpriteButton()
    {
        EmptySink.SetActive(false);
        FullSink.SetActive(false);
        cancel.gameObject.SetActive(false);
    }

    private void OnMouseUp()
    {
        FindObjectOfType<PipePuzzleHandler>().SpriteButton();
        if (pp.completed)
        {
            FullSink.SetActive(true);
        }
        else
        {
            EmptySink.SetActive(true);
        }
        cancel.gameObject.SetActive(true);
    }

}
