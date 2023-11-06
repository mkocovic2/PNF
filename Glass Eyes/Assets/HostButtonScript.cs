using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class HostButtonScript : MonoBehaviourPunCallbacks
{
    public InputField roomNameInputField;

    public void HostGame()
    {
        string roomName = roomNameInputField.text;

        // Check if the room name is valid (you can add more validation if needed).
        if (string.IsNullOrEmpty(roomName))
        {
            Debug.Log("Please enter a valid room name.");
            return;
        }

        // Try to create the room with the given room name.
        PhotonNetwork.CreateRoom(roomName);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created successfully with the room name: " + PhotonNetwork.CurrentRoom.Name);
        // Load the RoomCreationScene to allow players to join the room.
        PhotonNetwork.LoadLevel("RoomCreationScene");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room. Error: " + message);
    }
}
