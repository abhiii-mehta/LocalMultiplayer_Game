using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraTarget;
    public Vector3 offset;
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (cameraTarget != null)
        {
            Vector3 desiredPosition = cameraTarget.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

            // (Optional) Always look at the target from above (not needed for now)
            // transform.LookAt(cameraTarget);
        }
    }
}
