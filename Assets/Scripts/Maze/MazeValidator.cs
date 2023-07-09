using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeValidator
{
    public static List<Vector2Int> Path { get; private set; }

    /// <summary>
    /// Calculate the path from current position to destination postion, 
    /// the path result will be sotred in public static class variable `MazeValidator.Path`
    /// </summary>
    /// <param name="currentPos">current position</param>
    /// <param name="destPos">destination position</param>
    /// <param name="maze">maze</param>
    /// <returns>ture if path is found. other wise, return false</returns>
    public static bool CalculatePath(Vector2Int currentPos, Vector2Int destPos, WallState[,] maze)
    {
        // ------------------------------
        // BFS
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

            // next step
            int newSteps = steps[pos.x, pos.y] + 1;
            Vector2Int newPos;

            // check up
            if (!maze[pos.x, pos.y].HasFlag(WallStat.UP)) {
                if (newSteps < targetSteps || targetSteps == -1) {
                    newPos = pos + new Vector2Int(0, 1);
                    steps[newPos.x, newPos.y] = newSteps;
                    traceBackMap[newPos.x, newPos.y] = pos;
                    queue.Enqueue(newPos);
                }
            }

            // check down
            if (!maze[pos.x, pos.y].HasFlag(WallStat.LEFT)) {
                if (newSteps < targetSteps || targetSteps == -1) {
                    newPos = pos + new Vector2Int(-1, 0);
                    steps[newPos.x, newPos.y] = newSteps;
                    traceBackMap[newPos.x, newPos.y] = pos;
                    queue.Enqueue(newPos);
                }
            }

            //! ---- Special case: DOWN and RIGHT ----
            //* Due to same ... reason, the down case and right case need some sepcial condition
            // check DOWN
                //* down condition: check if down block has upper wall
            if (!maze[pos.x, pos.y - 1].HasFlag(WallStat.UP)) {
                if (newSteps < targetSteps || targetSteps == -1) {
                    newPos = pos + new Vector2Int(0, -1);
                    steps[newPos.x, newPos.y] = newSteps;
                    traceBackMap[newPos.x, newPos.y] = pos;
                    queue.Enqueue(newPos);
                }
            }

            // check RIGHT
                //* right condition: check if right block has left wall
            if (!maze[pos.x +1, pos.y].HasFlag(WallStat.LEFT)) {
                if (newSteps < targetSteps || targetSteps == -1) {
                    newPos = pos + new Vector2Int(1, 0);
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

        // path found, return true
        return true;
    }
}