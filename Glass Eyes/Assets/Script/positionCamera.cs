using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class positionCamera : MonoBehaviour
{
    public Transform cameraTarget;
    PhotonView PV;
    void Update()
    {
        if (!PV.IsMine)
            return;
        transform.position = cameraTarget.position;
    }
}
