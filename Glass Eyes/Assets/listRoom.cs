using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class listRoom : MonoBehaviour
{
    public TMP_Text lobbyText;
    public RoomInfo info;
    public void setupRoom(RoomInfo _info)
    {
        info = _info;
        lobbyText.text = _info.Name;
    }
    public void OnClick()
    {
        ServerConnect.instance.JoinRoom(info);
    }
}
