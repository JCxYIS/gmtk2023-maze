using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeValidator
{
    public static List<Vector2Int> Path { get; private set; }

    public static bool CalculatePath(Vector2Int currentPos, Vector2Int destPos, WallState[,] maze)
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
        int[,] steps = new int[rows, cols];        // init to 0 by default
        Queue<Vector2Int> queue = new Queue<Vector2Int>();

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                steps[i, j] = -1;
            }
        }

        // start bfs
        queue.Enqueue(currentPos);     
        steps[currentPos.x, currentPos.y] = 0;

        while (queue.Count != 0) {
            Vector2Int pos = queue.Dequeue();

            // found the destination
            if (pos == destPos) {
                break;
            }

            int newSteps = steps[pos.x, pos.y] + 1;
            for (int i = 0; i < 4; i++) {
                WallState state = directStatArr[i];
                Vector2Int direction = directVecArr[i];
                Vector2Int newPos = pos + direction;
                int targetSteps = steps[newPos.x, newPos.y];

                // no wall AND newPos not visited
                if (!maze[pos.x, pos.y].HasFlag(state) 
                        && (newSteps < targetSteps || targetSteps == -1)) {

                    steps[newPos.x, newPos.y] = newSteps;
                    traceBackMap[newPos.x, newPos.y] = pos;
                    queue.Enqueue(newPos);
                }
            }
        }

        // path not found
        if (steps[destPos.x, destPos.y] == -1) {
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