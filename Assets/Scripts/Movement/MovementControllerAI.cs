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
        InvokeRepeating("RefreshTarget", 1f, 1f);
        
    }

    void RefreshTarget()
    {
        var result = _agent.SetDestination(FindObjectOfType<DestBlock>().transform.position);
        print(result);
    }
}