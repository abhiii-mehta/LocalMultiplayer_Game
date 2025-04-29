using UnityEngine;
using System.Collections;

public class DebugTeleport : MonoBehaviour
{
    public Transform[] checkpointPositions;
    public Transform startPosition;
    public Transform player1;
    public Transform player2;

    private int currentCheckpointIndex = 0;

    private void Start()
    {
        StartCoroutine(FindCheckpointsDelayed());
    }

    private IEnumerator FindCheckpointsDelayed()
    {
        yield return new WaitForSeconds(0.5f); // wait for spawner

        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        GameObject goalPad = GameObject.FindGameObjectWithTag("GoalPad");

        checkpointPositions = new Transform[checkpoints.Length + 1];

        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpointPositions[i] = checkpoints[i].transform;
        }

        if (goalPad != null)
        {
            checkpointPositions[checkpointPositions.Length - 1] = goalPad.transform;
        }

        Debug.Log($"[DebugTeleport] Found {checkpointPositions.Length} checkpoints including goal pad.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SmartTeleport();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            TeleportToStart();
        }
    }

    private void SmartTeleport()
    {
        if (checkpointPositions == null || checkpointPositions.Length == 0) return;

        if (currentCheckpointIndex >= checkpointPositions.Length)
        {
            Debug.Log("[DebugTeleport] No more checkpoints left!");
            return;
        }

        Transform nextCheckpoint = checkpointPositions[currentCheckpointIndex];
        TeleportTo(nextCheckpoint);

        // If we teleported to GoalPad
        if (currentCheckpointIndex == checkpointPositions.Length - 1)
        {
            GoalPad goalPad = FindObjectOfType<GoalPad>();
            if (goalPad != null)
            {
                goalPad.ForceWin();  // Call new method
            }
        }


        currentCheckpointIndex++;
    }

    private void TeleportTo(Transform target)
    {
        Vector3 offsetY = Vector3.up * 2f; // small lift
        player1.position = target.position + Vector3.left + offsetY;
        player2.position = target.position + Vector3.right + offsetY;

        ApplyFakeGravity(player1);
        ApplyFakeGravity(player2);
    }

    private void TeleportToStart()
    {
        Vector3 offsetY = Vector3.up * 2f;
        player1.position = startPosition.position + Vector3.left + offsetY;
        player2.position = startPosition.position + Vector3.right + offsetY;

        ApplyFakeGravity(player1);
        ApplyFakeGravity(player2);

        currentCheckpointIndex = 0; // Reset progress
    }

    private void ApplyFakeGravity(Transform player)
    {
        // Trigger "ForceFall" manually if available
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.ForceFall();
        }
    }
}
