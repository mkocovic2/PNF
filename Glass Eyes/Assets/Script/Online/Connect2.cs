using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Connect2 : MonoBehaviourPunCallbacks
{
    public GameObject Player;
    [Space]
    public Transform Spawn;

    void Start()
    {
        Debug.Log("Connecting");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected To Server!");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("Test", null, null);

        Debug.Log("Player is connected");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        GameObject _Player = PhotonNetwork.Instantiate(Player.name, Spawn.position, Quaternion.identity);
        _Player.GetComponent<playerSetup>().IsLocalPlayer();
    }
}
