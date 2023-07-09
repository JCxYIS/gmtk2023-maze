using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeValidator
{
    public static List<Vector2Int> Path { get; private set; }

    public bool CalculatePath(Vector2Int currentPos, Vector2Int destPos, WallState[,] maze)
    {
        // TODO route

        // if(maze[0,1].HasFlag(WallState.UP))
        // {
        //     ...
        // }

        // ------------------------------
        // DFS
        // ------------------------------
        int rows = maze.GetLength(0);
        int cols = maze.GetLength(0);
        WallState[] directStatArr = {WallState.UP, WallState.LEFT, WallState.DOWN, WallState.RIGHT};
        Vector2Int[] directVecArr = {new Vector2Int(0, 1), new Vector2Int(-1, 0), new Vector2Int(0, -1), new Vector2Int(1, 0)};
        Vector2Int[,] traceBackMap = new Vector2Int[rows, cols];
        int[,]  isVisited = new int[rows, cols];        // init to 0 by default
        Queue<Vector2Int> queue = new Queue<Vector2Int>();

        // start dfs
        queue.Enqueue(currentPos);     
        isVisited[currentPos.x, currentPos.y] = 1;

        while (queue.Count != 0) {
            Vector2Int pos = queue.Dequeue();

            // found the destination
            if (pos == destPos) {
                break;
            }

            for (int i = 0; i < 4; i++) {
                WallState state = directStatArr[i];
                Vector2Int direction = directVecArr[i];
                Vector2Int newPos = pos + direction;

                // no wall AND newPos not visited
                if (!maze[pos.x, pos.y].HasFlag(state) && isVisited[newPos.x, newPos.y] == 0) {
                    isVisited[newPos.x, newPos.y] = 1;
                    traceBackMap[newPos.x, newPos.y] = pos;
                    queue.Enqueue(newPos);
                }
            }
        }

        // path not found
        if (isVisited[destPos.x, destPos.y] == 0) {
            return false;
        }

        // traceback the path
        Path = new List<Vector2Int>(); // FIXME store result here
        Vector2Int backPos = destPos;

        while (backPos != currentPos) {
            Path.Add(backPos);
            backPos = traceBackMap[backPos.x, backPos.y];
        }
        Path.Add(currentPos);
        // Path.Reverse();

        return true;
    }
}