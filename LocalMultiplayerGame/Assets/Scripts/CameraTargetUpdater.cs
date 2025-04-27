using UnityEngine;

public class CameraTargetUpdater : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public Vector3 backwardOffset = new Vector3(0f, 0f, -5f);

    void Update()
    {
        if (player1 != null && player2 != null)
        {
            Vector3 midpoint = (player1.position + player2.position) / 2f;
            transform.position = midpoint + backwardOffset;
        }
    }
}
