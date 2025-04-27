using UnityEngine;

public class GameStartHider : MonoBehaviour
{
    public GameObject youWinPanel;
    public GameObject pausePanel;

    void Start()
    {
        if (youWinPanel != null)
            youWinPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }
}
