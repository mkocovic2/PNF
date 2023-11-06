using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBlast : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Transform targetPoint;

    // Update is called once per frame
    void Update()
    {
        // Check for user input to shoot the bullet (e.g., pressing a button)
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate a new bullet object from the bullet prefab at the current position and rotation of the shooter
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Calculate the direction towards the target point
        Vector3 direction = (targetPoint.position - transform.position).normalized;

        // Get the rigidbody component of the bullet
        Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();

        if (bulletRigidbody != null)
        {
            // Set the velocity of the bullet towards the target point and bullet speed
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }
}

