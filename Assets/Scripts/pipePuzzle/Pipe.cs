using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public bool isSource;
    public bool hasWater;
    public Transform[] PipeOpenings;

    public Sprite Empty;
    public Sprite Full;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        setSprite();
    }

    private void setSprite()
    {
        if (hasWater) GetComponent<SpriteRenderer>().sprite = Full;
        else GetComponent<SpriteRenderer>().sprite = Empty;
    }

    public bool CheckConnection(Vector2 connectionDir)
    {
        if (!hasWater) return false;
        foreach(Transform opening in PipeOpenings)
        {
            Vector2 dir = opening.up;
            print(connectionDir + " " + dir);
            if (-dir == connectionDir)
            {

                return true;
            }
        }
        return false;
    }
}
