using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;

    public Transform player1;
    public Transform player2;
    public Transform fallbackStartPoint; // Starting point if no checkpoint reached

    private Transform currentCheckpoint;
    public Image fadeImage; // Black full-screen image for fading
    public float fadeDuration = 1f;

    private bool isRespawning = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public void PlayerFell()
    {
        if (!isRespawning)
        {
            StartCoroutine(RespawnSequence());
        }
    }

    private IEnumerator RespawnSequence()
    {
        isRespawning = true;

        // Fade to black
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            yield return StartCoroutine(FadeToBlack());
        }

        // Wait briefly
        yield return new WaitForSeconds(0.5f);

        // Teleport players
        Transform respawnPoint = currentCheckpoint != null ? currentCheckpoint : fallbackStartPoint;

        player1.position = respawnPoint.position + Vector3.left;
        player2.position = respawnPoint.position + Vector3.right;

        // Reset gravity triggers or any fall logic here if needed (optional)

        // Fade back
        if (fadeImage != null)
        {
            yield return StartCoroutine(FadeFromBlack());
            fadeImage.gameObject.SetActive(false);
        }

        isRespawning = false;
    }

    private IEnumerator FadeToBlack()
    {
        float t = 0;
        Color color = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1;
        fadeImage.color = color;
    }

    private IEnumerator FadeFromBlack()
    {
        float t = 0;
        Color color = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        color.a = 0;
        fadeImage.color = color;
    }
}
