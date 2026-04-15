using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    private Vector3 respawnPosition = new Vector3(10.5f, 36.24f, -1.44f);
    public AudioClip deathSound; // drag your sound here in Inspector

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            // Play death sound
            AudioSource.PlayClipAtPoint(deathSound, transform.position);

            // Reset position and velocity
            collision.gameObject.transform.position = respawnPosition;
            rb.linearVelocity = Vector3.zero;
        }
    }
}
