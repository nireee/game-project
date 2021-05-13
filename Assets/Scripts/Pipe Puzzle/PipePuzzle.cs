using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePuzzle : MonoBehaviour
{
    public int Width = 3;
    public int Height = 4;

    public List<Pipe> Pipes;

    public Pipe StartPipe, EndPipe;
    public bool completed;

    void Start()
    {
        HandlePipePath();
    }

    
    void Update()
    {
        if (!completed && EndPipe.HasWater) completed = true;
    }

    public bool CanTouch(Pipe pipe) {
        if (!completed && pipe != StartPipe && pipe != EndPipe) return true;
        else return false;
    }


    public void HandlePipePath()
    {
        Pipe source = null;
        foreach (Pipe pipe in Pipes)
        {
            if (pipe.isSource)
            {
                pipe.HasWater = true;
                source = pipe;
            }
            else
            {
                pipe.HasWater = false;
            }
        }

        List<Pipe> frontier = new List<Pipe>(); // only have pipes with water
        List<Pipe> visited = new List<Pipe>(); // all pipes 

        frontier.Add(source);

        while (frontier.Count > 0)
        {
            Pipe current = frontier[0];
            frontier.RemoveAt(0);
            visited.Add(current);
            List<Pipe> neighbors = getPipeNeighbors(current);
            foreach (Pipe neighbor in neighbors)
            {
                if (!visited.Contains(neighbor) && checkForWater(neighbor))
                {
                    neighbor.HasWater = true;
                    frontier.Add(neighbor);
                }

            }

        }
    }

    private List<Pipe> getPipeNeighbors(Pipe pipe)
    {
        int pipeID = Pipes.IndexOf(pipe);
        int x = pipeID % Width;
        int y = pipeID / Width;
        List<Pipe> neighbors = new List<Pipe>();

        if (getPipe(x + 1, y)) neighbors.Add(getPipe(x + 1, y));
        if (getPipe(x - 1, y)) neighbors.Add(getPipe(x - 1, y));
        if (getPipe(x, y + 1)) neighbors.Add(getPipe(x, y + 1));
        if (getPipe(x, y - 1)) neighbors.Add(getPipe(x, y - 1));

        return neighbors;

    }

    public bool checkForWater(Pipe pipe) {

        if (pipe.isSource) return true;

        int pipeID = Pipes.IndexOf(pipe);
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
            return Pipes[pipeID];
        }
        else
            return null;
        
    }

    private bool checkConnection(Pipe pipe, int x, int y, Vector2 dir)
    {
        bool hasMatchOpening = false;
        foreach(Transform opening in pipe.PipeOpenings)
        {
            if((int)opening.up.y == (int)dir.y && (int)opening.up.x == (int)dir.x) {

                hasMatchOpening = true;
            }
        }
        if (hasMatchOpening)
        {
            Pipe neighbor = getPipe(x + (int)dir.x, y - (int)dir.y);
            if (neighbor) return neighbor.CheckConnection(dir);
        }

        return false;
    }
}
