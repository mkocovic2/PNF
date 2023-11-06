using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

public class playerCam : MonoBehaviour
{
    public float sensitivityX;
    public float sensitivityY;
    public Transform orientation;

    private float xRotation;
    private float yRotation;
    public PhotonView PV;
    public GameObject playerCamera;
    public Transform cameraHolder;

    public Slider sensitivitySlider; 
    public float minSensitivity = 1f;
    public float maxSensitivity = 400f;

    void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(playerCamera);
        }
        ChangeSensitivity();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ChangeSensitivity()
    {
        if (PV.IsMine)
        {
            sensitivityX = timeVar.playerSens;
            sensitivityY = timeVar.playerSens;
        }
    }

    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
