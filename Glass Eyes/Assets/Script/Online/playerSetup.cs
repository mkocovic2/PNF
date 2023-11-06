using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerSetup : MonoBehaviourPun
{
    public playerMovement movement;
    public rotateModel rotate;
    public playerHealthController health;
    public GameObject mainCamera;
    public GameObject grapple;
    public GameObject laser;
    public GameObject laserBlast;

    private void Start()
    {
        if (!photonView.IsMine) return;
        IsLocalPlayer();

    }
    public void IsLocalPlayer()
    {
        health.enabled = true;
        laserBlast.SetActive(true);
        mainCamera.SetActive(true);
        laser.SetActive(true);
        grapple.SetActive(true);
    }
}
