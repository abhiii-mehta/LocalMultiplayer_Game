using UnityEngine;

public class GoalPad : MonoBehaviour
{
    private bool player1OnPad = false;
    private bool player2OnPad = false;

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

    void CheckWinCondition()
    {
        if (player1OnPad && player2OnPad)
        {
            Debug.Log("LEVEL COMPLETE!!! ");
        }
    }
}
