using UnityEngine;

public class Tether : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    private LineRenderer line;

    public float maxStretchDistance = 7f;
    public float warningStretchDistance = 5f;
    public Color normalColor = Color.cyan;
    public Color warningColor = Color.yellow;
    public Color breakColor = Color.red;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    void Update()
    {
        if (player1 != null && player2 != null)
        {
            line.SetPosition(0, player1.position);
            line.SetPosition(1, player2.position);

            float distance = Vector3.Distance(player1.position, player2.position);

            if (distance > maxStretchDistance)
            {
                Debug.Log("Link Broken!");
                SetLineColor(breakColor);
             
            }
            else if (distance > warningStretchDistance)
            {
                SetLineColor(warningColor);
            }
            else
            {
                SetLineColor(normalColor);
            }
        }
    }

    void SetLineColor(Color color)
    {
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(color, 0.0f), new GradientColorKey(color, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
        );
        line.colorGradient = gradient;
    }
}
