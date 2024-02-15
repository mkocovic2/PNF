using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class createGrapple : MonoBehaviour
{
    public Transform grapplePosition;
    public GameObject grappleGun; 
    void Start()
    {
        PhotonNetwork.Instantiate(grappleGun.name, grapplePosition.position, Quaternion.identity);
    }
}
