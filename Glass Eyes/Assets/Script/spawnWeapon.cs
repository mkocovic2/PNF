using UnityEngine;
using Photon.Pun;

public class spawnWeapon : MonoBehaviourPunCallbacks
{
    public GameObject weaponPrefab;
    private GameObject instantiatedWeapon;

    private void Start()
    {
        if (photonView.IsMine)
        {
            instantiatedWeapon = Instantiate(weaponPrefab, transform.position, transform.rotation, transform);
            instantiatedWeapon.SetActive(false);
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (!photonView.IsMine && PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("ActivateWeaponForRemoteClients", RpcTarget.OthersBuffered);
        }
    }

    [PunRPC]
    private void ActivateWeaponForRemoteClients()
    {
        instantiatedWeapon.SetActive(true);
    }
}
