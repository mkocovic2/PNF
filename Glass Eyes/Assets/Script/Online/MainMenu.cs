using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using JetBrains.Annotations;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public InputField RoomInput;
    public InputField JoinInput;

    public void createRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(RoomInput.text, roomOptions);
    }
    public void joinRoom()
    {
        PhotonNetwork.JoinRoom(JoinInput.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("LavaOne");
    }
}
