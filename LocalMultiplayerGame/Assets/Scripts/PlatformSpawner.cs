using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject checkpointPrefab;
    public GameObject goalPadPrefab;
    public GameObject youWinPanel;

    public int platformsPerStage = 10;
    public float distanceBetween = 4f;

    public float heightVariationEasy = 0.5f;
    public float heightVariationMedium = 1.0f;
    public float heightVariationHard = 1.5f;

    void Start()
    {
        Vector3 spawnPosition = transform.position;

        // --- Stage 1 (Easy) ---
        for (int i = 0; i < platformsPerStage; i++)
        {
            float randomHeight = Random.Range(-heightVariationEasy, heightVariationEasy);
            spawnPosition.y += randomHeight;

            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            spawnPosition.z += distanceBetween;
        }

        // Spawn Checkpoint 1
        GameObject checkpoint1 = Instantiate(checkpointPrefab, spawnPosition, Quaternion.identity);
        checkpoint1.tag = "Checkpoint"; // <<< TAG IT AFTER SPAWNING
        spawnPosition.z += distanceBetween;

        // --- Stage 2 (Medium) ---
        for (int i = 0; i < platformsPerStage; i++)
        {
            float randomHeight = Random.Range(-heightVariationMedium, heightVariationMedium);
            spawnPosition.y += randomHeight;

            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            spawnPosition.z += distanceBetween;
        }

        // Spawn Checkpoint 2
        GameObject checkpoint2 = Instantiate(checkpointPrefab, spawnPosition, Quaternion.identity);
        checkpoint2.tag = "Checkpoint"; // <<< TAG IT AFTER SPAWNING
        spawnPosition.z += distanceBetween;

        // --- Stage 3 (Hard) ---
        for (int i = 0; i < platformsPerStage; i++)
        {
            float randomHeight = Random.Range(-heightVariationHard, heightVariationHard);
            spawnPosition.y += randomHeight;

            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            spawnPosition.z += distanceBetween;
        }

        // Spawn GoalPad
        GameObject goalPadInstance = Instantiate(goalPadPrefab, spawnPosition, Quaternion.identity);
        goalPadInstance.tag = "GoalPad"; // <<< TAG IT AFTER SPAWNING

        GoalPad goalPadScript = goalPadInstance.GetComponent<GoalPad>();
        if (goalPadScript != null)
        {
            goalPadScript.youWinPanel = youWinPanel;
        }
    }
}
