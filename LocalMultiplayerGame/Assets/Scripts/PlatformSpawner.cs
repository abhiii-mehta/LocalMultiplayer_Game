using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject goalPadPrefab; 
    public int numberOfPlatforms = 10;
    public float distanceBetween = 4f;
    public float heightVariation = 1f;

    void Start()
    {
        Vector3 spawnPosition = transform.position;

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            float randomHeight = Random.Range(-heightVariation, heightVariation);
            spawnPosition.y += randomHeight;

            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

            spawnPosition.z += distanceBetween;
        }

        Instantiate(goalPadPrefab, spawnPosition, Quaternion.identity);
    }
}
