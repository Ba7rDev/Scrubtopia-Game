using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI scoreText;
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
}