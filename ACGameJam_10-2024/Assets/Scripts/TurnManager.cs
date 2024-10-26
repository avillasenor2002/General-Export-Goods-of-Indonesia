using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    public int maxInputs = 5;
    public float restartDelay = 2.0f;
    public TextMeshProUGUI inputText;
    public Image fadeImage;
    public float fadeDuration = 1.5f;

    private int inputCount = 0;

    void Start()
    {
        UpdateInputText();
        SetFadeAlpha(0);  // Ensure fadeImage starts transparent
        CheckEnemiesRemaining(); // Initial check for enemy count
    }

    void Update()
    {
        // Check for any input
        if (Input.anyKeyDown)
        {
            CountInput();
        }

        // Continuously check for the number of active enemies
        CheckEnemiesRemaining();
    }

    void CountInput()
    {
        inputCount++;
        UpdateInputText();

        // Check if input count has reached the maximum threshold
        if (inputCount >= maxInputs)
        {
            StartCoroutine(RestartSceneAfterDelay());
        }
    }

    void UpdateInputText()
    {
        int inputsRemaining = maxInputs - inputCount;
        inputText.text = $"{inputsRemaining}";
    }

    void CheckEnemiesRemaining()
    {
        int enemyCount = FindObjectsOfType<EnemyScript>().Length;

        if (enemyCount <= 0)
        {
            // Trigger fade to black and restart once enemies are all gone
            StartCoroutine(RestartSceneAfterDelay());
        }
    }

    IEnumerator RestartSceneAfterDelay()
    {
        Debug.Log($"Max inputs reached or no enemies left. Restarting scene in {restartDelay} seconds...");

        // Start fade to black
        yield return StartCoroutine(FadeToBlack());

        // Wait for the specified delay
        yield return new WaitForSeconds(restartDelay);

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator FadeToBlack()
    {
        float elapsed = 0f;

        // Gradually increase the alpha of the fadeImage
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            SetFadeAlpha(alpha);
            yield return null;
        }

        SetFadeAlpha(1);  // Ensure it ends fully black
    }

    void SetFadeAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
        }
    }
}
