using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePuzzle : MonoBehaviour
{
    public int Width = 3;
    public int Height = 4;
    public List<Pipe> pipes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Pipe pipe in pipes)
        {
            pipe.hasWater = checkForWater(pipe);

        }
    }

    public bool checkForWater(Pipe pipe)
    {
        if (pipe.isSource) return true;
        int pipeID = pipes.IndexOf(pipe);
        int x = pipeID % Width;
        int y = pipeID / Width;
        if (checkConnection(pipe, x, y, new Vector2(1, 0))) return true;
        if (checkConnection(pipe, x, y, new Vector2(-1, 0))) return true;
        if (checkConnection(pipe, x, y, new Vector2(0, 1))) return true;
        if (checkConnection(pipe, x, y, new Vector2(0, -1))) return true;
        return false;
    }

    public Pipe getPipe(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            int pipeID = x + y * Width;
            return pipes[pipeID];
        }
        else
            return null;
    }

    private bool checkConnection(Pipe pipe, int x, int y, Vector2 dir)
    {
        bool hasMatchingOpening = false;
        foreach(Transform opening in pipe.PipeOpenings)
        {
            //print(opening.up + " " + dir);
            if((int)opening.up.y == (int)dir.y && (int)opening.up.x == (int)dir.x)
            {
                hasMatchingOpening = true;
            }
        }

        if (hasMatchingOpening)
        {
            //flip the y dir 
            Pipe neighbor = getPipe(x + (int)dir.x, y - (int)dir.y);
            if (neighbor) return neighbor.CheckConnection(dir);
        }
        
        return false;
    }
}
