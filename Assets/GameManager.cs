using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Required for scene loading

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public GameObject deathPanel; // Assign your DeathPanel GameObject here in the Inspector

    private int score = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevents duplicate managers across scene loads
        }
    }

    public void AddScore(int amount)
    {
        score += amount;

        // NULL CHECK: Prevents MissingReferenceException if ScoreText is destroyed
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // Call this method when Mario dies
    public void ShowDeathScreen()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
        }
    }

    // Attach this method to the Respawn Button's OnClick event
    public void RespawnGame()
    {
        // Reloads the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Attach this method to the Quit Button's OnClick event
    public void QuitToMainMenu()
    {
        // Loads the Main Menu scene
        SceneManager.LoadScene("MainMenu");
    }
}