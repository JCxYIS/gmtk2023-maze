using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallBuilderController : MonoBehaviour
{
    Camera _cam;

    [SerializeField]
    Transform _cursor;


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
            // _cursor.transform.position = position;
            SetCursorPos(position);
        }


        if (Input.GetMouseButtonDown(0))
        {
            // TODO
            print("Click at "+_cursor.transform.position);
        }
    }

    void SetCursorPos(Vector3 pos)
    {
        float x = pos.x;
        float z = pos.z;
        
        float disToLEFT = Mathf.Abs(x - Mathf.Round(x)+0.5f);
        float disToRIGHT = Mathf.Abs(x - Mathf.Round(x)-0.5f);
        float disToUP = Mathf.Abs(z - Mathf.Round(z)+0.5f);
        float disToDOWN = Mathf.Abs(z - Mathf.Round(z)-0.5f);

        float min = Mathf.Min(disToLEFT, disToRIGHT, disToUP, disToDOWN);

        if (min == disToLEFT)
        {
            x = Mathf.Round(x)-0.5f;
            z = Mathf.Round(z);
            _cursor.eulerAngles = Vector3.zero;
        }
        else if (min == disToRIGHT)
        {
            x = Mathf.Round(x)+0.5f;
            z = Mathf.Round(z);
            _cursor.eulerAngles = Vector3.zero;
        }
        else if (min == disToUP)
        {
            x = Mathf.Round(x);
            z = Mathf.Round(z)-0.5f;  
            _cursor.eulerAngles = new Vector3(0, 90f, 0);
        }
        else if (min == disToDOWN)
        {
            x = Mathf.Round(x);
            z = Mathf.Round(z)+0.5f;
            _cursor.eulerAngles = new Vector3(0, 90f, 0);
        }

        _cursor.transform.position = new Vector3(x, pos.y, z);
        // print($"{pos} => {_cursor.transform.position}");
    }
}