using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeValidator
{
    public List<Vector2> Path { get; private set; }

    public bool CalculatePath(WallState[,] maze)
    {
        // TODO route

        // if(maze[0,1].HasFlag(WallState.UP))
        // {
        //     ...
        // }

        // result path
        Path = new List<Vector2>(); // FIXME store result here
        return false;
    }
}