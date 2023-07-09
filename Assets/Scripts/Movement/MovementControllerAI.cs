using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class MovementControllerAI : MonoBehaviour
{
    NavMeshAgent _agent;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();        
    }

    void RefreshTarget()
    {
        // TODO change timing (not every 1s)
        NavMeshPath navMeshPath = new NavMeshPath();
        Vector3 dest = FindObjectOfType<DestBlock>().transform.position;
        if (_agent.CalculatePath(dest, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            _agent.SetPath(navMeshPath);
        }
        else
        {
            Debug.Log("No possible path!");
        }
    }
}