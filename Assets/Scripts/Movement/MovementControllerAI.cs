using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class MovementControllerAI : MonoBehaviour
{
    [SerializeField]
    float _moveSpeed = 0.2f;

    MazeController _mazeController;
    // NavMeshAgent _agent;
    Stack<Vector2Int> _plannedPath = new Stack<Vector2Int>();

    
    void Awake()
    {
        // _agent = GetComponent<NavMeshAgent>();
        _mazeController = FindObjectOfType<MazeController>();
        StartCoroutine(UpdateCoroutine());
    }

    IEnumerator UpdateCoroutine()
    {
        while(true)
        {
            if(_plannedPath.Count == 0)
            {
                yield return null;
                continue;
            }

            Vector2Int nextPos = _plannedPath.Pop();
            Vector3 nextPos3D = new Vector3(nextPos.x, 0, nextPos.y);
            print($"Go to {nextPos} (real pos:{nextPos3D})");
            // _agent.SetDestination(nextPos3D);
            // move
            while(Vector3.Distance(transform.position, nextPos3D) >= 0.001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPos3D, _moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        _mazeController.OnMazeChanged += RefreshTarget;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        _mazeController.OnMazeChanged -= RefreshTarget;
    }

    void RefreshTarget()
    {
        _plannedPath = new Stack<Vector2Int>(_mazeController.Path);

        string s = "";
        foreach(var p in _mazeController.Path)
        {
            s += "("+p.x+","+p.y+") \n";
        }
        print("Path: "+s);
    }

    // void RefreshTarget()
    // {        
    //     NavMeshPath navMeshPath = new NavMeshPath();
    //     Vector3 dest = FindObjectOfType<DestBlock>().transform.position;
    //     if (_agent.CalculatePath(dest, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
    //     {
    //         _agent.SetPath(navMeshPath);
    //         print("Refreshed path!");
    //     }
    //     else
    //     {
    //         Debug.Log("No possible path!");
    //     }
    // }
}