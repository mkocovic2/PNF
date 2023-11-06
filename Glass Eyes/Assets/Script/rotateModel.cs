using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class rotateModel : MonoBehaviour
{
    public Transform targetObject;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (view.IsMine)
        {
            float targetRotationY = targetObject.rotation.eulerAngles.y;

            Vector3 currentRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(currentRotation.x, targetRotationY, currentRotation.z);
        }
    }
}