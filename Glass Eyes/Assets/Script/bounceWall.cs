using UnityEngine;

public class bounceWall : MonoBehaviour
{
    public float bounceForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is the player (you can use tags or layers for this check).
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player's rigidbody component.
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRigidbody != null)
            {
                // Calculate the direction to bounce the player.
                Vector3 bounceDirection = Vector3.Reflect(collision.relativeVelocity.normalized, collision.contacts[0].normal).normalized;

                // Calculate the force to be applied.
                Vector3 bounceForceVector = bounceDirection * bounceForce;

                // Add the force to the player's rigidbody.
                playerRigidbody.AddForce(bounceForceVector, ForceMode.Impulse);
            }
        }
    }
}
