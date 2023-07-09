using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallBuilderController : MonoBehaviour
{

    [SerializeField]
    Transform _cursor;

    float cd = 0;

    Camera _cam;
    MazeController _mazeController;
    MeshRenderer _cursorRenderer;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _cam = Camera.main;
        _mazeController = FindObjectOfType<MazeController>();
        _cursorRenderer = _cursor.GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, 100f))
        {
            return;
        }

        // find cell
        Vector3 position = hit.point;        
        int cellI = Mathf.RoundToInt(position.x);
        int cellJ = Mathf.RoundToInt(position.z);
        WallState side = FindSide(position);      // which side is nearest  

        // if side is RIGHT or DOWN, check if i, j fits the criteria (最右邊才有 RIGHT、最下邊才有 DOWN)
        if(side == WallState.RIGHT)
        {
            if(cellI != _mazeController.Width-1)
            {
                cellI += 1;
                side = WallState.LEFT;
            }
        }
        else if(side == WallState.DOWN)
        {
            if(cellJ != 0)
            {
                cellJ -= 1;
                side = WallState.UP;
            }
        }

        var cell = _mazeController.GetCell(cellI, cellJ);

        // determine action for wall/no wall
        if((cell & side) != 0) // has wall
        {
            Wall wall = _mazeController.GetWall(cellI, cellJ, side);
            _cursor.transform.position = wall.transform.position;
            _cursor.eulerAngles = wall.transform.eulerAngles;
            _cursorRenderer.material.color = Color.red;

            // handle click
            if (Input.GetMouseButtonDown(0))
            {
                // cd check
                if(GameController.Instance.ConstructCd >= 0)
                {
                    return;
                }                
                print("Tear down wall "+cellI+", "+cellJ+"  "+side);
                _mazeController.TearDownWall(cellI, cellJ, side);
                GameController.Instance.SetConstructCd();
                GameController.Instance.AddWalls(1);
            }
        }
        else // no wall
        {
            float offsetI = side == WallState.LEFT ? -.5f : (side == WallState.RIGHT ? .5f : 0);
            float offsetJ = side == WallState.DOWN ? -.5f : (side == WallState.UP ? .5f : 0);
            _cursor.transform.position = new Vector3(cellI+offsetI, 0, cellJ+offsetJ);
            _cursor.eulerAngles = new Vector3(0, (side == WallState.UP || side == WallState.DOWN) ? 90 : 0, 0);
             _cursorRenderer.material.color = Color.green;

            // handle click
            if (Input.GetMouseButtonDown(0))
            {
                // cd check
                if(GameController.Instance.ConstructCd >= 0 || GameController.Instance.Walls <= 0)
                {
                    return;
                }
                print("Build wall "+cellI+", "+cellJ+"  "+side);
                _mazeController.BuildWall(cellI, cellJ, side);
                GameController.Instance.SetConstructCd();
                GameController.Instance.AddWalls(-1);
            }
        }

        
    }

    WallState FindSide(Vector3 pos)
    {
        float x = pos.x;
        float z = pos.z;        
        float disToLEFT = Mathf.Abs(x - Mathf.Round(x)+0.5f);
        float disToUP = Mathf.Abs(z - Mathf.Round(z)-0.5f);
        float disToRIGHT = Mathf.Abs(x - Mathf.Round(x)-0.5f);
        float disToDOWN = Mathf.Abs(z - Mathf.Round(z)+0.5f);
        // find which side is nearest
        float min = Mathf.Min(disToLEFT, disToRIGHT, disToUP, disToDOWN);
        if (min == disToLEFT)
            return WallState.LEFT;
        else if (min == disToRIGHT)
            return WallState.RIGHT;        
        else if (min == disToUP)
            return WallState.UP;
        else
            return WallState.DOWN;
    }

    
}