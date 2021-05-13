using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePuzzle : MonoBehaviour
{
    public int Width = 3;
    public int Height = 4;
    public List<Pipe> pipes;

    public Pipe StartPipe, EndPipe;
    public bool Completed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        HandlePipePath();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Completed && EndPipe.hasWater) Completed = true;
    }

    public bool CanTouch(Pipe pipe)
    {
        if(!Completed && pipe != StartPipe && pipe != EndPipe)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void HandlePipePath()
    {
        Pipe source = null;
        foreach (Pipe pipe in pipes)
        {
            //pipe.hasWater = checkForWater(pipe);

            if (pipe.isSource)
            {
                pipe.hasWater = true;
                source = pipe;
            }
            else
            {
                pipe.hasWater = false;
            }
        }

        List<Pipe> frontier = new List<Pipe>(); //will only have pipe with water
        List<Pipe> visited = new List<Pipe>(); 
        frontier.Add(source);
        while(frontier.Count > 0)
        {
            Pipe current = frontier[0];
            frontier.RemoveAt(0);
            visited.Add(current);
            List<Pipe> neighbors = getPipeNeighbors(current);
            foreach(Pipe neighbor in neighbors)
            {
                if(!visited.Contains(neighbor) && checkForWater(neighbor))
                {
                    neighbor.hasWater = true;
                    frontier.Add(neighbor);
                }
            }
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

    private List<Pipe> getPipeNeighbors(Pipe pipe)
    {
        int pipeID = pipes.IndexOf(pipe);
        int x = pipeID % Width;
        int y = pipeID / Width;
        List<Pipe> neighbors = new List<Pipe>();
        if (getPipe(x + 1, y)) neighbors.Add(getPipe(x + 1, y));
        if (getPipe(x - 1, y)) neighbors.Add(getPipe(x - 1, y));
        if (getPipe(x, y + 1)) neighbors.Add(getPipe(x, y + 1));
        if (getPipe(x, y - 1)) neighbors.Add(getPipe(x, y - 1));
        return neighbors;
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
