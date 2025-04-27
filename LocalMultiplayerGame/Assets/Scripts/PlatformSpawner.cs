using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int numberOfPlatforms = 10;
    public float distanceBetween = 4f;
    public float heightVariation = 1f;

    void Start()
    {
        Vector3 spawnPosition = transform.position;

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            // Random height variation
            float randomHeight = Random.Range(-heightVariation, heightVariation);
            spawnPosition.y += randomHeight;

            // Instantiate platform
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

            // Move FORWARD on Z-axis instead of sideways on X-axis
            spawnPosition.z += distanceBetween;
        }
    }
}
