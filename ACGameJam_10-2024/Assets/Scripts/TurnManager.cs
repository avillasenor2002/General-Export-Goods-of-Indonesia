using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TurnManager : MonoBehaviour
{
    public int maxInputs = 5;
    public float restartDelay = 2.0f;
    public TextMeshProUGUI inputText;
    public Image fadeImage;
    public float fadeDuration = 1.5f;

    private int inputCount = 0;
    private int initialEnemyCount;
    public event Action<int> OnInputCountChanged;

    // variables added by eb!
    [Header("Progress Bar Items")]
    public PauseMenuManager pauseMenuManager;
    private int starsEarned = -1;

    public Slider progressSlider;
    public GameObject progressStar1, progressStar2, progressStar3;
    private float progressStarY = -2.5f;

    public BallMovement Balls;

    [Header("Goals for Each Star Achievement in Percent of Progress\n(IN ORDER SMALLEST TO BIGGEST)")]

    [Range(0, 1)]
    public float progressGoal1;
    [Range(0, 1)]
    public float progressGoal2;
    [Range(0, 1)]
    public float progressGoal3;

    void Start()
    {
        inputCount = 0;
        PositionProgressStars();
        Balls = FindObjectOfType<BallMovement>();

        UpdateInputText();
        SetFadeAlpha(0);  // Ensure fadeImage starts transparent
        initialEnemyCount = FindObjectsOfType<EnemyScript>().Length;
        UpdateProgressBar(); // Initial update for progress bar
        CheckEnemiesRemaining(); // Initial check for enemy count

        StartCoroutine(InitializeInputCount());
    }

    IEnumerator InitializeInputCount()
    {
        yield return new WaitForSeconds(0.1f);

        OnInputCountChanged?.Invoke(inputCount);
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

    public void CountInput()
    {
        Debug.Log("Counting Input");
        inputCount = Balls.turnAmount;
        UpdateInputText();
        OnInputCountChanged?.Invoke(inputCount);

        // Check if input count has reached the maximum threshold
        if (inputCount >= maxInputs)
        {
            ShowEndScreen(starsEarned);  // Show loss screen instead of immediately fading to black
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
            ShowEndScreen(starsEarned);
        }

        UpdateProgressBar(); // Update progress bar based on remaining enemies
    }

    // added by eb!
    public void ShowEndScreen(int finalStarsEarned)
    {
        // call the menu manager to show the end screen with the amount of stars achieved
        pauseMenuManager.GoToVictoryScreen(finalStarsEarned);

        // fade to black
        StartCoroutine(FadeToBlack());
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

    // added by eb!
    // positions the stars of the progress bar (called at scene start)
    private void PositionProgressStars()
    {
        // position star 1
        float posXStar1 = (progressGoal1 * progressSlider.maxValue) - 100f;
        Debug.Log("Position of star 1 is: " + posXStar1);
        progressStar1.GetComponent<RectTransform>().anchoredPosition = new Vector2(posXStar1, progressStarY);

        // position star 2
        float posXStar2 = (progressGoal2 * progressSlider.maxValue) - 100f;
        progressStar2.GetComponent<RectTransform>().anchoredPosition = new Vector2(posXStar2, progressStarY);

        // position star 3
        float posXStar3 = (progressGoal3 * progressSlider.maxValue) - 100f;
        progressStar3.GetComponent<RectTransform>().anchoredPosition = new Vector2(posXStar3, progressStarY);
    }

    // updated by eb!
    void UpdateProgressBar()
    {
        if (progressSlider != null && initialEnemyCount > 0)
        {
            // check amount of enemies remaining
            int remainingEnemies = FindObjectsOfType<EnemyScript>().Length;
            // find percent of progress made
            float progressPercent = 1.0f - (float)remainingEnemies / initialEnemyCount;
            // update slider value to reflect percent
            progressSlider.value = progressSlider.maxValue * progressPercent;

            // check if player earned star(s)
            int newStarsEarned = 0;
            if (progressPercent >= progressGoal1)
            {
                newStarsEarned = 1;
            }

            if (progressPercent >= progressGoal2)
            {
                newStarsEarned = 2;
            }

            if (progressPercent >= progressGoal3)
            {
                newStarsEarned = 3;
            }

            UpdateProgressStars(newStarsEarned);
        }
    }

    // added by eb!
    // shows progress stars if the player has earned more
    private void UpdateProgressStars(int newStarsEarned)
    {
        if (newStarsEarned == starsEarned)
        {
            return;
        }

        starsEarned = newStarsEarned;
        Debug.Log("Player earned a total of " + starsEarned + " stars.");
        // ADD STAR CHANGES HERE
        switch (starsEarned)
        {
            case 0:
                progressStar1.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                progressStar2.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                progressStar3.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                break;
            case 1:
                progressStar1.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 0.92f, 0.016f, 1f);
                progressStar2.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                progressStar3.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                break;
            case 2:
                progressStar1.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 0.92f, 0.016f, 1f);
                progressStar2.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 0.92f, 0.016f, 1f);
                progressStar3.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                break;
            case 3:
                progressStar1.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 0.92f, 0.016f, 1f);
                progressStar2.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 0.92f, 0.016f, 1f);
                progressStar3.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1f, 0.92f, 0.016f, 1f);
                break;
            default:
                Debug.Log("Error: Amount of stars earned is not applicable to progress.");
                break;
        }
    }
}


