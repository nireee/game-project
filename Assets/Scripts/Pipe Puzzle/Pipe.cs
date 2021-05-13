using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public bool isSource;
    public bool HasWater;
    public Transform[] PipeOpenings;
    public Sprite Empty;
    public Sprite Full;


    void Start()
    {
        
    }

    
    void Update()
    {
        SetSprite();
    }

    private void SetSprite()
    {
        if (HasWater) GetComponent<SpriteRenderer>().sprite = Full;
        else GetComponent<SpriteRenderer>().sprite = Empty;
    }

    public bool CheckConnection(Vector2 connectionDir)
    {
        if (!HasWater) return false;
        foreach(Transform opening in PipeOpenings)
        {
            Vector2 dir = opening.up;
            if (-dir == connectionDir) return true;
        }

        return false;
    }

    private void OnMouseDown()
    {
        if (FindObjectOfType<PipePuzzle>().CanTouch(this)){

            transform.Rotate(new Vector3(0, 0, -90));
            FindObjectOfType<PipePuzzle>().HandlePipePath();
        }
        
    }
}
