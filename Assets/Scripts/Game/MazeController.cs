using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class MazeController : MonoBehaviour
{
    [SerializeField]
    [Range(5, 25)]
    private int width = 10;

    [SerializeField]
    [Range(5, 25)]
    private int height = 10;


    [SerializeField]
    private Transform wallPrefab = null;
    [SerializeField]
    private Transform startPosPrefab = null;
    [SerializeField]
    private Transform destPosPrefab = null;

    [SerializeField]
    private Transform mazeSolverTransform = null;

    
    private WallState[,] _maze;
    private Dictionary<(int, int, WallState), Wall> _walls = new Dictionary<(int, int, WallState), Wall>();

    public int Width => width;
    public int Height => height;
    public UnityAction OnMazeChanged;
    public List<Vector2Int> Path => MazeValidator.Path;


    #region monobehaviour
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // generate maze
        int seed = Random.Range(int.MinValue, int.MaxValue);
        print("Seed: "+seed);        
        _maze = MazeGenerator.Generate(width, height, seed);

        // render maze
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                var cell = _maze[i, j];
                var position = new Vector3(i, 0, j);

                if (cell.HasFlag(WallState.UP))
                {
                    BuildWall(i, j, WallState.UP, false);
                }

                if (cell.HasFlag(WallState.LEFT))
                {
                    BuildWall(i, j, WallState.LEFT, false);
                }

                if (i == width - 1) // 最右邊才有 RIGHT
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        BuildWall(i, j, WallState.RIGHT, false);
                    }
                }

                if (j == 0) // 最下邊才有 DOWN
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        BuildWall(i, j, WallState.DOWN, false);
                    }
                }
            }
        }
        // startpos and endpos
        Instantiate(startPosPrefab, transform).position = new Vector3(0, 0, 0);
        Instantiate(destPosPrefab, transform).position = new Vector3(width-1, 0, height-1);
        MazeValidator.CalculatePath(new Vector2Int(0, 0), new Vector2Int(width-1, height-1), _maze);
        OnMazeChanged?.Invoke();
    }
    #endregion

    public WallState GetCell(int i, int j)
    {
        if (i < 0 || i >= width || j < 0 || j >= height)
        {
            return 0;
        }

        return _maze[i, j];
    }

    public Wall GetWall(int i, int j, WallState state)
    {
        return _walls[(i, j, state)];
    }


    public Wall BuildWall(int i, int j, WallState state, bool doValidate = true)
    {
        // perhaps add single flag check here? lazy lol

        // check dest is reachable
        if(doValidate)
        {
            MazeValidator tmp_validator = new MazeValidator();
            Vector2Int playerCell = new Vector2Int(Mathf.RoundToInt(mazeSolverTransform.position.x), Mathf.RoundToInt(mazeSolverTransform.position.z));
            
            _maze[i, j] |= state;
            if(!MazeValidator.CalculatePath(playerCell, new Vector2Int(width-1, height-1), _maze))
            {
                _maze[i, j] &= ~state;  // remove flag
                throw new System.InvalidOperationException($"Dest will be unreachable if build wall at {i} {j} {state}");
            }
        }

        var wall = Instantiate(wallPrefab, transform).GetComponent<Wall>();
        wall.Init(i, j, state);
        _walls.Add((i, j, state), wall);
        if(doValidate)
            OnMazeChanged?.Invoke();
        return wall;
    }

    public void TearDownWall(int i, int j, WallState state)
    {
        // perhaps add single flag check here? lazy lol

        var wall = _walls[(i, j, state)];
        if(wall == null)
            throw new System.Exception($"No wall at {i} {j} {state}");
        
        wall.MarkDestroyed();
        _maze[i, j] &= ~state;  // remove flag
        OnMazeChanged?.Invoke();
        _walls.Remove((i,j,state));
    }
}