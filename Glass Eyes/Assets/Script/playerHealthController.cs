using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class playerHealthController : MonoBehaviourPun
{
    public int maxHealth = 100;
    public int respawnTime = 5;
    private int currentHealth;

    public static playerHealthController instance;
    public Image HealthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        HealthBar.fillAmount = 1f;
    }

    [PunRPC]
    public void takeDamageFunction(int _damage)
    {
        if (!photonView.IsMine) return;

        currentHealth -= _damage;
        HealthBar.fillAmount = (float)currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            photonView.RPC("Die", RpcTarget.All);
        }
    }

    [PunRPC]
    private void Die()
    {
        if (!photonView.IsMine) return;

        photonView.RPC("DeactivatePlayer", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void DeactivatePlayer()
    {
        gameObject.SetActive(false);

        if (photonView.IsMine)
        {
            matchManager.instance.RespawnPlayer(gameObject, respawnTime);
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        HealthBar.fillAmount = 1f;
    }

    private IEnumerator RespawnAfterDelay(GameObject playerObject, float delay)
    {
        yield return new WaitForSeconds(delay);

        Transform spawnPoint = matchManager.instance.GetRandomSpawnPoint();
        playerObject.transform.position = spawnPoint.position;
        playerObject.transform.rotation = spawnPoint.rotation;

        playerObject.SetActive(true);

        playerHealthController playerHealth = playerObject.GetComponent<playerHealthController>();
        if (playerHealth != null)
        {
            playerHealth.ResetHealth();
        }

        Rigidbody playerRigidbody = playerObject.GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
        }
    }
}
