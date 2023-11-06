using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatePlayer : MonoBehaviour
{
    public Transform targetObject;

    private void Update()
    {
        float targetRotationY = targetObject.rotation.eulerAngles.y;

        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(currentRotation.x, targetRotationY, currentRotation.z);
    }
}
