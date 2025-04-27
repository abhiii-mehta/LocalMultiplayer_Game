using UnityEngine;

public class TetherPuller : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public Rigidbody rb1;
    public Rigidbody rb2;

    public float pullStartDistance = 4f; // Start pulling after 4 units
    public float maxPullDistance = 8f;    // Stronger pull after 7-8 units
    public float basePullForce = 10f;     // Base pull force

    void Update()
    {
        if (player1 == null || player2 == null || rb1 == null || rb2 == null)
            return;

        float currentDistance = Vector3.Distance(player1.position, player2.position);

        if (currentDistance > pullStartDistance)
        {
            Vector3 direction1 = (player2.position - player1.position).normalized;
            Vector3 direction2 = (player1.position - player2.position).normalized;

            float stretchAmount = currentDistance - pullStartDistance;
            float forceMultiplier = stretchAmount / (maxPullDistance - pullStartDistance);
            forceMultiplier = Mathf.Clamp01(forceMultiplier); // Keep it between 0 and 1

            float finalPullForce = basePullForce * forceMultiplier;

            // Only pull horizontally (XZ), don't affect Y
            direction1.y = 0;
            direction2.y = 0;

            rb1.AddForce(direction1 * finalPullForce, ForceMode.Force);
            rb2.AddForce(direction2 * finalPullForce, ForceMode.Force);
        }
    }
}
