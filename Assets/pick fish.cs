using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip pickupSound; // ✅ drag your sound here in Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Poisson récupéré !");

            // ✅ Play sound at the position of the fish before destroying it
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            Destroy(gameObject);
            GameManager.instance.WinGame();
        }
    }
}