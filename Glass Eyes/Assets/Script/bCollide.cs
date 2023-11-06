using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class bCollide : MonoBehaviour
{
    public float speed = 20f; // Bullet travel speed
    public int damage = 10; // Damage value of the bullet

    private void Start()
    {
        // Destroy the bullet after a set amount of time to prevent it from flying forever
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        // Move the bullet forward based on its speed
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // Handle collisions with other objects
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Hit!");
            PhotonView PV = collision.gameObject.GetComponent<PhotonView>();
            PV.RPC("takeDamageFunction", RpcTarget.All, damage);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
