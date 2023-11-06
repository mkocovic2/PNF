using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Fusion.NetworkCharacterController;

public class lava : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PhotonView photonView = other.GetComponent<PhotonView>();
        if (photonView != null && photonView.CompareTag("Player"))
        {
            photonView.RPC("takeDamageFunction", RpcTarget.All, 100);
        }
    }
}
