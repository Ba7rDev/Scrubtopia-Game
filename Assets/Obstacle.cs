using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    private bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(HandlePlayerDeath(other.gameObject));
        }
    }

    IEnumerator HandlePlayerDeath(GameObject player)
    {
        // 1. Disable player movement script so it stops moving
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = false;

        // 2. Hide player sprite and disable colliders (Keeps camera & audio active!)
        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        Collider2D col = player.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // 3. Play death sound
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.deathSound);
        }

        // 4. Wait for the sound to finish before reloading
        yield return new WaitForSeconds(0.8f);

        // 5. Restart scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}