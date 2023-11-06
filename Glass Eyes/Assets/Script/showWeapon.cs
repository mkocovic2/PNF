using Fusion;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showWeapon : MonoBehaviourPun
{
    public Transform grappleLocation;
    public GameObject grapplingHook;
    public Transform weaponHolder; 

    public void Start()
    {
        GameObject newGrapple = PhotonNetwork.Instantiate(grapplingHook.name, grappleLocation.transform.position, grappleLocation.transform.rotation);

        newGrapple.transform.SetParent(weaponHolder);

        newGrapple.SetActive(true);
    }
}
