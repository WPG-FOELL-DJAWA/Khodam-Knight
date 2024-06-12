using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Camera _targetCamera;

    private void OnEnable()
    {
        _targetCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 targetPosition = _targetCamera.transform.position;

        transform.LookAt(targetPosition);
    }
}
