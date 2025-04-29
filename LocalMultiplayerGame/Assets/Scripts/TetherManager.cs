using UnityEngine;

public class TetherManager : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public LineRenderer line;

    public float breakDistance = 10f;
    public float dangerDistance = 8f;
    public float pullStartDistance = 6f;

    private bool tetherBroken = false;

    private void Update()
    {
        if (tetherBroken || player1 == null || player2 == null || line == null)
            return;

        float distance = Vector3.Distance(player1.position, player2.position);

        if (distance > breakDistance)
        {
            tetherBroken = true;
            line.enabled = false;
            return;
        }

        line.SetPosition(0, player1.position);
        line.SetPosition(1, player2.position);

        Color color = Color.cyan;
        if (distance > dangerDistance)
            color = Color.red;
        else if (distance > pullStartDistance)
            color = Color.yellow;

        line.startColor = color;
        line.endColor = color;
    }

    public void ResetTether(Transform newP1, Transform newP2, Rigidbody newRb1, Rigidbody newRb2)
    {
        player1 = newP1;
        player2 = newP2;

        if (line != null)
        {
            line.enabled = true;
            line.startColor = Color.cyan;
            line.endColor = Color.cyan;
        }

        if (TryGetComponent<TetherPuller>(out TetherPuller puller))
        {
            puller.player1 = newP1;
            puller.player2 = newP2;
            puller.rb1 = newRb1;
            puller.rb2 = newRb2;
        }

        UpdateLine();
        tetherBroken = false;
    }

    private void UpdateLine()
    {
        if (line != null && player1 != null && player2 != null)
        {
            line.SetPosition(0, player1.position);
            line.SetPosition(1, player2.position);
        }
    }
}
