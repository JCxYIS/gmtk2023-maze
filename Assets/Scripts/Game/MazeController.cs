using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeController : MonoBehaviour
{
    [SerializeField]
    [Range(1, 50)]
    private int width = 10;

    [SerializeField]
    [Range(1, 50)]
    private int height = 10;

    [SerializeField]
    private float wallThickness = 1f;


    [SerializeField]
    private Transform wallPrefab = null;
    [SerializeField]
    private Transform startPosPrefab = null;
    [SerializeField]
    private Transform destPosPrefab = null;

    
    private WallState[,] _maze;


    #region monobehaviour
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        int seed = Random.Range(int.MinValue, int.MaxValue);
        print("Seed: "+seed);
        _maze = MazeGenerator.Generate(width, height, seed);

        // render maze
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                var cell = _maze[i, j];
                var position = new Vector3(-width/2+i, 0, -height/2+j);

                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0, 0.5f);
                    topWall.localScale = new Vector3(wallThickness, topWall.localScale.y, topWall.localScale.z);
                    topWall.eulerAngles = new Vector3(0, 90, 0);
                    topWall.name = $"Wall ({i}. {j}) UP";
                }

                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-0.5f, 0, 0);
                    leftWall.localScale = new Vector3(wallThickness, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.name = $"Wall ({i}. {j}) LEFT";
                }

                if (i == width - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(0.5f, 0, 0);
                        rightWall.localScale = new Vector3(wallThickness, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.name = $"Wall ({i}. {j}) RIGHT";
                    }
                }

                if (j == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, 0, -0.5f);
                        bottomWall.localScale = new Vector3(wallThickness, bottomWall.localScale.y, bottomWall.localScale.z);
                        bottomWall.eulerAngles = new Vector3(0, 90, 0);
                        bottomWall.name = $"Wall ({i}. {j}) BOTTOM";
                    }
                }
            }
        }
        // startpos and endpos
        Instantiate(startPosPrefab, transform).position = new Vector3(-width/2, 0, -height/2);
        Instantiate(destPosPrefab, transform).position = new Vector3(width/2-1, 0, height/2-1);
    }
    #endregion

    public WallState GetCell(int i, int j)
    {
        if (i < 0 || i >= width || j < 0 || j >= height)
        {
            throw new System.IndexOutOfRangeException();
        }

        return _maze[i, j];
    }
}