using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class MovementControllerAI : MonoBehaviour
{
    MazeController _mazeController;
    NavMeshAgent _agent;

    
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _mazeController = FindObjectOfType<MazeController>();
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
        NavMeshPath navMeshPath = new NavMeshPath();
        Vector3 dest = FindObjectOfType<DestBlock>().transform.position;
        if (_agent.CalculatePath(dest, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            _agent.SetPath(navMeshPath);
            print("Refreshed path!");
        }
        else
        {
            Debug.Log("No possible path!");
        }
    }
}