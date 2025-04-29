using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;

    public Transform player1;
    public Transform player2;
    public Transform fallbackStartPoint; // Starting point if no checkpoint reached

    public GameObject tetherPrefab;
    private GameObject currentTether;

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

        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            yield return StartCoroutine(FadeToBlack());
        }

        yield return new WaitForSeconds(0.5f);

        Transform respawnPoint = currentCheckpoint != null ? currentCheckpoint : fallbackStartPoint;

        player1.position = respawnPoint.position + Vector3.left;
        player2.position = respawnPoint.position + Vector3.right;

        if (fadeImage != null)
        {
            yield return StartCoroutine(FadeFromBlack());
            fadeImage.gameObject.SetActive(false);
        }

        RebuildTether();

        isRespawning = false;
    }

    private void RebuildTether()
    {
        // After teleporting players
        if (currentTether != null)
            Destroy(currentTether);

        currentTether = Instantiate(tetherPrefab);

        // Reconnect systems
        TetherManager tetherManager = currentTether.GetComponent<TetherManager>();
        TetherPuller tetherPuller = currentTether.GetComponent<TetherPuller>();

        if (tetherManager != null)
        {
            tetherManager.player1 = player1;
            tetherManager.player2 = player2;
            tetherManager.line = currentTether.GetComponent<LineRenderer>();
            tetherManager.ResetTether(player1, player2, player1.GetComponent<Rigidbody>(), player2.GetComponent<Rigidbody>());

        }

        if (tetherPuller != null)
        {
            tetherPuller.player1 = player1;
            tetherPuller.player2 = player2;
            tetherPuller.rb1 = player1.GetComponent<Rigidbody>();
            tetherPuller.rb2 = player2.GetComponent<Rigidbody>();
        }

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
