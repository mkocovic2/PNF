using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraRotation : MonoBehaviour
{
    public Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    private void Update()
    {
        transform.rotation = cameraTransform.rotation;
    }
}
