using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrdinaryBlock : MonoBehaviour
{
    bool stepped = false;
    [SerializeField] Material _steppedMaterial;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!stepped)
            {
                GameController.Instance.AddScore(100);                
                GameController.Instance.TouchedBlocks++;
                GetComponent<MeshRenderer>().material = _steppedMaterial;
            }
            else
            {
                GameController.Instance.AddScore(-1);
            }
            stepped = true;
        }
    }
}