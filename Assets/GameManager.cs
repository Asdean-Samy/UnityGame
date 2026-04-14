using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI victoryText;
    public AudioClip victorySound;
    private bool gameWon = false; // ✅ track if game is won

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        victoryText.gameObject.SetActive(false);
    }

    void Update()
    {
        // ✅ Only allow reset after winning
        if (gameWon && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void WinGame()
    {
        victoryText.gameObject.SetActive(true);
        AudioSource.PlayClipAtPoint(victorySound, Camera.main.transform.position);
        gameWon = true; // ✅ enable the reset
    }
}