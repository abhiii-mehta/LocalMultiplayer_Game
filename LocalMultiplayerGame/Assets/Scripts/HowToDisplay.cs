using UnityEngine;

public class ShowHowToPlay : MonoBehaviour
{
    public GameObject howToPlayText;

    void Start()
    {
        if (howToPlayText != null)
        {
            howToPlayText.SetActive(true);
            Invoke(nameof(HideText), 4f);
        }
    }

    void HideText()
    {
        howToPlayText.SetActive(false);
    }
}
