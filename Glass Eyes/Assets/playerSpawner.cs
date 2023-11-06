using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class playerSpawner : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints;

    private void Start()
    {
        // Check if we are the master client (host) and the game has started
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            SpawnPlayers();
        }
    }

    private void SpawnPlayers()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points found. Make sure you have assigned spawn points to the PlayerSpawner.");
            return;
        }

        Player[] players = PhotonNetwork.PlayerList;
        int spawnPointIndex = 0;

        foreach (Player player in players)
        {
            // Instantiate the player prefab and assign ownership based on player's actor number
            GameObject newPlayer = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[spawnPointIndex].position, Quaternion.identity);
            newPlayer.GetComponent<PhotonView>().TransferOwnership(player.ActorNumber);

            // Increment spawnPointIndex and wrap around to 0 if it exceeds the number of spawn points
            spawnPointIndex = (spawnPointIndex + 1) % spawnPoints.Length;
        }
    }
}
