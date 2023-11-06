using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    PhotonView PV;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;
        PlayerInput();
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
        MovePlayer();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}