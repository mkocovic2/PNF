using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTest : MonoBehaviour
{
    [SerializeField] float mouseSensitivity;
    public GameObject cameraHolder;
    float verticalLookRotation;
    public PhotonView PV;
    public GameObject playerCam;

    void Start()
    {
        
    }

    void Update()
    {
        if (!PV.IsMine)
        {
            Destroy(playerCam);
        }
            
        Look();
    }
    void Look()
    {
        if (!PV.IsMine)
        {
            return;
        }
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }
}
