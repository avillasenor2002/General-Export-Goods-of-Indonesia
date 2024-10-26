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
    public Image progressBar;             // Reference to the progress bar UI Image
    public GameObject victoryScreen;      // Reference to the victory screen GameObject
    public GameObject lossScreen;         // Reference to the loss screen GameObject
    public float fadeDuration = 1.5f;

    private int inputCount = 0;
    private int initialEnemyCount;

    void Start()
    {
        UpdateInputText();
        SetFadeAlpha(0);  // Ensure fadeImage starts transparent
        initialEnemyCount = FindObjectsOfType<EnemyScript>().Length;
        UpdateProgressBar(); // Initial update for progress bar
        CheckEnemiesRemaining(); // Initial check for enemy count

        // Ensure victory and loss screens are hidden initially
        if (victoryScreen != null)
            victoryScreen.SetActive(false);

        if (lossScreen != null)
            lossScreen.SetActive(false);
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
            ShowLossScreen();  // Show loss screen instead of immediately fading to black
        }
    }

    void UpdateInputText()
    {
        int inputsRemaining = maxInputs - inputCount;
        inputText.text = $"TURN {inputsRemaining}";
    }

    void CheckEnemiesRemaining()
    {
        int enemyCount = FindObjectsOfType<EnemyScript>().Length;

        if (enemyCount <= 0)
        {
            ShowVictoryScreen();
        }

        UpdateProgressBar(); // Update progress bar based on remaining enemies
    }

    void ShowVictoryScreen()
    {
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);

            // Optional: Start the fade-to-black effect for the background
            StartCoroutine(FadeToBlack());
        }
    }

    void ShowLossScreen()
    {
        if (lossScreen != null)
        {
            lossScreen.SetActive(true);

            // Optional: Start the fade-to-black effect for the background
            StartCoroutine(FadeToBlack());
        }
    }

    IEnumerator RestartSceneAfterDelay()
    {
        Debug.Log($"Restarting scene in {restartDelay} seconds...");

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

    void UpdateProgressBar()
    {
        if (progressBar != null && initialEnemyCount > 0)
        {
            int remainingEnemies = FindObjectsOfType<EnemyScript>().Length;
            float progress = 1.0f - (float)remainingEnemies / initialEnemyCount;
            progressBar.fillAmount = progress;
        }
    }
}
