using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public GameObject groundPrefab;
    public GameObject coinPrefab;
    public Transform playerTransform;

    public float platformWidth = 10f;
    public float spawnDistanceAhead = 25f;

    private float nextSpawnX = 0f;

    void Start()
    {
        // Spawn initial safe ground platforms ahead at start
        for (int i = 0; i < 4; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        // As player runs, continuously spawn new platforms infinitely ahead
        if (playerTransform != null && playerTransform.position.x + spawnDistanceAhead > nextSpawnX)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        // 1. Spawn Ground Platform
        GameObject newGround = Instantiate(groundPrefab, new Vector2(nextSpawnX, -3f), Quaternion.identity);

        // 2. Randomly spawn coins on this platform (80% chance)
        if (Random.value < 0.8f && coinPrefab != null)
        {
            // Pick a random number of coins to spawn on this chunk (1 to 3 coins)
            int coinCount = Random.Range(1, 4);

            for (int i = 0; i < coinCount; i++)
            {
                // Random position along the platform
                float randomXOffset = Random.Range(-4f, 4f);
                // Random height (lower or higher jumping height)
                float randomYOffset = Random.Range(-1.5f, 0.5f);

                Vector2 coinPos = new Vector2(nextSpawnX + randomXOffset, randomYOffset);

                // Attach coin as child of ground platform so it cleans up automatically
                Instantiate(coinPrefab, coinPos, Quaternion.identity, newGround.transform);
            }
        }

        // 3. Advance to the next spawn X position
        nextSpawnX += platformWidth;

        // 4. Destroy ground (and all child coins/obstacles) after 20 seconds to save memory
        Destroy(newGround, 20f);
    }
}