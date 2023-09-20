using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }
    
    void Update()
    {
        Quaternion rotation = _mainCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
    }
}
