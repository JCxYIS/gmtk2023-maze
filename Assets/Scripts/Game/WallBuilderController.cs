using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallBuilderController : MonoBehaviour
{
    Camera _cam;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _cam = Camera.main;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        var ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            var position = hit.point;
            // position.x = Mathf.Round(position.x);
            // position.z = Mathf.Round(position.z);
            transform.position = position;
        }

        if (Input.GetMouseButtonDown(0))
        {
            // TODO
            print("Click at "+transform.position);
        }
    }
}