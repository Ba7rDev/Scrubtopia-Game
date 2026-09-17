using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject groundPrefab;
    public GameObject coinPrefab;
    public GameObject obstaclePrefab;

    [Header("Platform Settings")]
    public Transform playerTransform;
    public float platformWidth = 10f;
    public float spawnDistanceAhead = 25f;

    [Header("Obstacle Settings")]
    [Range(0f, 1f)]
    public float obstacleSpawnChance = 0.5f; // 50% chance to spawn an obstacle
    public float obstacleYOffset = -2.2f;      // Adjust height to sit flush on top of ground

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
        // Continuously spawn platforms infinitely ahead as player moves
        if (playerTransform != null && playerTransform.position.x + spawnDistanceAhead > nextSpawnX)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        // 1. Spawn Ground Platform
        GameObject newGround = Instantiate(groundPrefab, new Vector2(nextSpawnX, -3f), Quaternion.identity);

        // 2. Randomly spawn coins on platform (80% chance)
        if (Random.value < 0.8f && coinPrefab != null)
        {
            int coinCount = Random.Range(1, 4);

            for (int i = 0; i < coinCount; i++)
            {
                float randomXOffset = Random.Range(-4f, 4f);
                float randomYOffset = Random.Range(-1.5f, 0.5f);

                Vector2 coinPos = new Vector2(nextSpawnX + randomXOffset, randomYOffset);

                // Attach coin as child of ground platform for auto-cleanup
                Instantiate(coinPrefab, coinPos, Quaternion.identity, newGround.transform);
            }
        }

        // 3. Randomly spawn cactus obstacle (skipped on starting platform)
        if (nextSpawnX > 0f && Random.value < obstacleSpawnChance && obstaclePrefab != null)
        {
            Vector2 obstaclePos = new Vector2(nextSpawnX + Random.Range(-3f, 3f), obstacleYOffset);

            // Spawn obstacle at its true world position and scale
            GameObject newObstacle = Instantiate(obstaclePrefab, obstaclePos, Quaternion.identity);

            // Attach to newGround without inheriting distorted ground scaling
            newObstacle.transform.SetParent(newGround.transform, true);
        }


        // 4. Advance X position for the next platform chunk
        nextSpawnX += platformWidth;

        // 5. Clean up ground platform and all child objects (coins/obstacles) after 20 seconds
        Destroy(newGround, 20f);
    }
}