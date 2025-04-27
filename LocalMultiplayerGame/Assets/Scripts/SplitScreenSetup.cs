using UnityEngine;

public class SplitScreenSetup : MonoBehaviour
{
    public bool isLeftSide = true;

    void Start()
    {
        Camera cam = GetComponent<Camera>();

        if (isLeftSide)
        {
            cam.rect = new Rect(0, 0, 0.5f, 1);
        }
        else
        {
            cam.rect = new Rect(0.5f, 0, 0.5f, 1);
        }
    }
}
