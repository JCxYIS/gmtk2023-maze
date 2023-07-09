using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeValidator
{
    public List<Vector2Int> Path { get; private set; }

    public bool CalculatePath(Vector2Int currentPos, Vector2Int destPos, WallState[,] maze)
    {
        // TODO route

        // if(maze[0,1].HasFlag(WallState.UP))
        // {
        //     ...
        // }

        // result path
        Path = new List<Vector2Int>(); // FIXME store result here
        return false;
    }
}