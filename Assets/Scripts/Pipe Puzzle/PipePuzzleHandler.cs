using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePuzzleHandler : MonoBehaviour
{

    public Sink sink;
    public PipePuzzle pp;
    public SpriteButton cancel;

    void Start()
    {
        cancel.gameObject.SetActive(false);
    }

    private void OnMouseUp()
    {
        sink.SpriteButton();
        pp.gameObject.SetActive(true);
        cancel.gameObject.SetActive(true);
    }

    public void SpriteButton()
    {
        cancel.gameObject.SetActive(false);
        pp.gameObject.SetActive(false);
    }
}
