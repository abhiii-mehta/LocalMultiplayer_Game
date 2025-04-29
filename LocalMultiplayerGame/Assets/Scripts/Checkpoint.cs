using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Vector3 lastCheckpointPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            Debug.Log("[Checkpoint] Player reached checkpoint!");

            // Tell RespawnManager this is the new checkpoint
            if (RespawnManager.instance != null)
            {
                RespawnManager.instance.SetCheckpoint(transform);
            }

            // Optional: You can destroy checkpoint or deactivate it after touched
            // gameObject.SetActive(false);
        }
    }

}
