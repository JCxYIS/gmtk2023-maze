using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;

public class MouseControll : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera _vcamera;

    [SerializeField]
    float _moveSpeedX = 1f;
    [SerializeField]
    float _moveSpeedY = 1f;
    [SerializeField]
    float _scrollSpeed = 1f;

    CinemachineOrbitalTransposer _orbitalTransposer;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _orbitalTransposer = _vcamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse X") < 0)
            {
                _orbitalTransposer.m_Heading.m_Bias += Input.GetAxisRaw("Mouse X") * _moveSpeedX;
            }

            if (Input.GetAxis("Mouse Y") > 0 || Input.GetAxis("Mouse Y") < 0)
            {
                _orbitalTransposer.m_FollowOffset.z += Input.GetAxisRaw("Mouse Y") * _moveSpeedY;

                if(_orbitalTransposer.m_FollowOffset.z < 0.01f)
                    _orbitalTransposer.m_FollowOffset.z = 0.01f;
            }
        }

        _orbitalTransposer.m_FollowOffset.y -= Input.GetAxis("Mouse ScrollWheel") * _scrollSpeed;
        if (_orbitalTransposer.m_FollowOffset.y < 5)
            _orbitalTransposer.m_FollowOffset.y = 5;
    }
}