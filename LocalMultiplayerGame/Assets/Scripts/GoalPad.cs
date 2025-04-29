using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPad : MonoBehaviour
{
    private bool player1OnPad = false;
    private bool player2OnPad = false;

    public GameObject youWinPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
            player1OnPad = true;

        if (other.CompareTag("Player2"))
            player2OnPad = true;

        CheckWinCondition();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1"))
            player1OnPad = false;

        if (other.CompareTag("Player2"))
            player2OnPad = false;
    }

    public void CheckWinCondition()
    {
        if (player1OnPad && player2OnPad)
        {
            Debug.Log("LEVEL COMPLETE!!!");

            if (youWinPanel != null)
            {
                youWinPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                Debug.LogError("YouWinPanel reference not assigned!");
            }
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ForceWin()
    {
        if (youWinPanel != null)
        {
            youWinPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

}
