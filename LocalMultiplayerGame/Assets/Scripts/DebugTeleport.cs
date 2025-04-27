using UnityEngine;
using System.Collections;

public class DebugTeleport : MonoBehaviour
{
    public Transform[] checkpointPositions;
    public Transform startPosition;
    public Transform player1;
    public Transform player2;
    public float distanceThreshold = 10f;
    private bool canTeleport = true;
    public float teleportCooldown = 0.5f;

    private int currentCheckpointIndex = 0;

    private void Start()
    {
        StartCoroutine(FindCheckpointsDelayed());
    }

    private IEnumerator FindCheckpointsDelayed()
    {
        // Wait a tiny bit so PlatformSpawner can spawn everything
        yield return new WaitForSeconds(0.5f);

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
        if (Input.GetKeyDown(KeyCode.T) && canTeleport)
        {
            StartCoroutine(TeleportCooldownRoutine(true));
        }

        if (Input.GetKeyDown(KeyCode.U) && canTeleport)
        {
            StartCoroutine(TeleportCooldownRoutine(false));
        }
    }

    private void TryTeleport()
    {
        if (checkpointPositions == null || checkpointPositions.Length == 0) return;

        Vector3 averagePosition = (player1.position + player2.position) / 2f;
        float playerZ = averagePosition.z;

        if (playerZ < checkpointPositions[0].position.z)
        {
            TeleportToCheckpoint(0); // Before checkpoint1
        }
        else if (playerZ < checkpointPositions[1].position.z)
        {
            TeleportToCheckpoint(1); // Between checkpoint1 and checkpoint2
        }
        else
        {
            TeleportToCheckpoint(2); // After checkpoint2
        }
    }

    private void TeleportToCheckpoint(int index)
    {
        if (index >= checkpointPositions.Length) return;

        Vector3 checkpointPos = checkpointPositions[index].position;
        Vector3 liftOffset = new Vector3(0, 3f, 0);
        player1.position = checkpointPos + Vector3.left;
        player2.position = checkpointPos + Vector3.right;

        Debug.Log($"[DebugTeleport] Teleported to checkpoint {index + 1}");
    }
    private IEnumerator TeleportCooldownRoutine(bool toNextCheckpoint)
    {
        canTeleport = false;

        if (toNextCheckpoint)
            TeleportToNextCheckpoint();
        else
            TeleportToStart();

        yield return new WaitForSeconds(teleportCooldown);

        canTeleport = true;
    }

    private void TeleportToNextCheckpoint()
    {
        if (checkpointPositions == null || checkpointPositions.Length == 0) return;

        if (currentCheckpointIndex >= checkpointPositions.Length)
        {
            Debug.Log("[DebugTeleport] No more checkpoints left!");
            return;
        }

        Vector3 checkpointPos = checkpointPositions[currentCheckpointIndex].position;

        Vector3 liftOffset = new Vector3(0, 1f, 0); // small lift up

        player1.position = checkpointPos + Vector3.left + liftOffset;
        player2.position = checkpointPos + Vector3.right + liftOffset;

        Rigidbody rb1 = player1.GetComponent<Rigidbody>();
        Rigidbody rb2 = player2.GetComponent<Rigidbody>();
        if (rb1 != null) rb1.linearVelocity = Vector3.zero;
        if (rb2 != null) rb2.linearVelocity = Vector3.zero;

        Debug.Log($"[DebugTeleport] Teleported to checkpoint {currentCheckpointIndex + 1}");

        currentCheckpointIndex++;
    }

    private void TeleportToStart()
    {
        Vector3 liftOffset = new Vector3(0, 1f, 0); // small lift up

        player1.position = startPosition.position + Vector3.left + liftOffset;
        player2.position = startPosition.position + Vector3.right + liftOffset;

        Rigidbody rb1 = player1.GetComponent<Rigidbody>();
        Rigidbody rb2 = player2.GetComponent<Rigidbody>();
        if (rb1 != null) rb1.linearVelocity = Vector3.zero;
        if (rb2 != null) rb2.linearVelocity = Vector3.zero;

        currentCheckpointIndex = 0;
        Debug.Log("[DebugTeleport] Teleported back to START");
    }

}
