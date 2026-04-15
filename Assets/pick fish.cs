using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip pickupSound; // drag your song here

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Poisson récupéré !");

            // sound at the pick up of the fish
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            Destroy(gameObject);
            GameManager.instance.WinGame();
        }
    }
}
