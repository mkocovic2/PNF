using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class matchManager : MonoBehaviourPunCallbacks
{
    public static matchManager instance;

    public GameObject playerPrefab;
    public Transform spawnPointsParent;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            SpawnPlayer();
        }
    }

    public void SpawnPlayer()
    {
        if (spawnPointsParent == null)
        {
            Debug.LogError("Spawn points parent is not assigned in the MatchManager script!");
            return;
        }

        Transform spawnPoint = GetRandomSpawnPoint();

        GameObject newPlayer = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);

        Grappling grapplingScript = newPlayer.GetComponent<Grappling>();
        if (grapplingScript != null)
        {
            grapplingScript.StopGrapple();
        }
    }

    public Transform GetRandomSpawnPoint()
    {
        return spawnPointsParent.GetChild(Random.Range(0, spawnPointsParent.childCount));
    }

    public void ResetGrapplingHook(GameObject playerObject)
    {
        Grappling grapplingScript = playerObject.GetComponent<Grappling>();
        if (grapplingScript != null)
        {
            grapplingScript.StopGrapple();
        }
    }

    public void RespawnPlayer(GameObject playerObject, float delay)
    {
        StartCoroutine(RespawnAfterDelay(playerObject, delay));
    }

    private IEnumerator RespawnAfterDelay(GameObject playerObject, float delay)
    {
        yield return new WaitForSeconds(delay);

        Transform spawnPoint = GetRandomSpawnPoint();
        playerObject.transform.position = spawnPoint.position;
        playerObject.transform.rotation = spawnPoint.rotation;

        playerObject.SetActive(true);

        playerHealthController playerHealth = playerObject.GetComponent<playerHealthController>();
        if (playerHealth != null)
        {
            playerHealth.ResetHealth();
        }

        ResetGrapplingHook(playerObject);

        Rigidbody playerRigidbody = playerObject.GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
        }
    }
}
