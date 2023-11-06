using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;

public class ServerConnect : MonoBehaviourPunCallbacks
{
    public static ServerConnect instance;
    public TMP_InputField roomName;
    public GameObject hostButton;
    public GameObject Connect;
    public GameObject joinLobby;
    public GameObject errorMenu;
    public GameObject mainMenu;
    public TMP_Text lobbyTitle;
    public Transform roomListContent;
    public GameObject roomPrefab;
    public GameObject roomList;

    public Transform playerList;
    public GameObject playerPrefab;

    public GameObject startGame;
    public GameObject mapArrows;

    public GameObject timeArrows;

    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();  
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Lobby");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Menu");
        SceneManager.LoadScene("MainMenu");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }
    public void CreateRoom()
    {
        Debug.Log("Create Room");
        if (string.IsNullOrEmpty(roomName.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomName.text);
        hostButton.SetActive(false);
        Connect.SetActive(true);
    }
    public override void OnJoinedRoom()
    {
        Connect.SetActive(false);
        joinLobby.SetActive(true);
        lobbyTitle.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach(Transform child in playerList)
        {
            Destroy(child.gameObject);
        }
                
        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerPrefab, playerList).GetComponent<playerList>().setupPlayer(players[i]);
        }
        startGame.SetActive(PhotonNetwork.IsMasterClient);
        mapArrows.SetActive(PhotonNetwork.IsMasterClient);
        timeArrows.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGame.SetActive(PhotonNetwork.IsMasterClient);
        mapArrows.SetActive(PhotonNetwork.IsMasterClient);
        timeArrows.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Connect.SetActive(false);
        errorMenu.SetActive(true);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        joinLobby.SetActive(false);
        Connect.SetActive(true);

    }
    public override void OnLeftRoom()
    {
        Connect.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void joinButton()
    {
        mainMenu.SetActive(false);
        roomList.SetActive(true);
        
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform obj in roomListContent)
        {
            Destroy(obj.gameObject);
        }
        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomPrefab, roomListContent).GetComponent<listRoom>().setupRoom(roomList[i]);
        }
    }
    

    public void JoinRoom(RoomInfo roomInfo)
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);
        roomList.SetActive(false);
        Connect.SetActive(true);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerPrefab, playerList).GetComponent<playerList>().setupPlayer(newPlayer);
    }
    
    public void startMatch()
    {
        PhotonNetwork.LoadLevel("lavaOne");
    }
    public void endMatch()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PhotonNetwork.LoadLevel("MainMenu");
        LeaveRoom();
    }
}
