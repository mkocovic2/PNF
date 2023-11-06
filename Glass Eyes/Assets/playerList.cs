using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class playerList : MonoBehaviourPunCallbacks
{
    Player player;
    public TMP_Text text;

    public void setupPlayer(Player _player)
    {
        player = _player;
        text.text = _player.NickName;
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
