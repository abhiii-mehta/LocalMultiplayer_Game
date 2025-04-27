using UnityEngine;

public class TetherManager : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public LineRenderer lineRenderer;

    public float breakDistance = 15f;
    public float dangerDistance = 8f;  // Distance at which color is fully red
    public float pullStartDistance = 4f; // Distance where color starts changing

    private Color calmColor = Color.blue;
    private Color warningColor = Color.yellow;
    private Color dangerColor = Color.red;

    void Update()
    {
        if (player1 == null || player2 == null || lineRenderer == null)
            return;

        // Update line positions
        lineRenderer.SetPosition(0, player1.position);
        lineRenderer.SetPosition(1, player2.position);

        float currentDistance = Vector3.Distance(player1.position, player2.position);

        UpdateTetherColor(currentDistance);

        // OPTIONAL: Break if stretched way too far
        if (currentDistance > breakDistance)
        {
            BreakTether();
        }
    }

    private void UpdateTetherColor(float distance)
    {
        if (distance < pullStartDistance)
        {
            // Calm Zone
            SetTetherColor(calmColor);
        }
        else if (distance >= pullStartDistance && distance < dangerDistance)
        {
            // Blend between Yellow based on stretch
            float t = (distance - pullStartDistance) / (dangerDistance - pullStartDistance);
            Color blendedColor = Color.Lerp(warningColor, dangerColor, t);
            SetTetherColor(blendedColor);
        }
        else
        {
            // Full danger color
            SetTetherColor(dangerColor);
        }
    }

    private void SetTetherColor(Color color)
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }

    private void BreakTether()
    {
        Debug.Log("[TetherManager] Tether broken!");
        Destroy(lineRenderer);
    }
}
