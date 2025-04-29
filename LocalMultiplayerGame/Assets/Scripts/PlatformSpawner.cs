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
        Vector3 checkpointOffset = new Vector3(7f, -4f, 2f);
        GameObject checkpoint1 = Instantiate(checkpointPrefab, spawnPosition + checkpointOffset, Quaternion.identity);

        checkpoint1.tag = "Checkpoint";
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
        GameObject checkpoint2 = Instantiate(checkpointPrefab, spawnPosition + checkpointOffset, Quaternion.identity);

        checkpoint2.tag = "Checkpoint";
        spawnPosition.z += distanceBetween;

        // --- Stage 3 (Hard) ---
        for (int i = 0; i < platformsPerStage; i++)
        {
            float randomHeight = Random.Range(-heightVariationHard, heightVariationHard);
            spawnPosition.y += randomHeight;

            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            spawnPosition.z += distanceBetween;
        }

        // Spawn GoalPad with Y-rotation to face players
        Vector3 goalOffset = new Vector3(6f, 2f, 2f);
        GameObject goalPadInstance = Instantiate(goalPadPrefab, spawnPosition + goalOffset, Quaternion.Euler(0, 90, 0));
        goalPadInstance.tag = "GoalPad";

        GoalPad goalPadScript = goalPadInstance.GetComponent<GoalPad>();
        if (goalPadScript != null)
        {
            goalPadScript.youWinPanel = youWinPanel;
        }
    }
}
