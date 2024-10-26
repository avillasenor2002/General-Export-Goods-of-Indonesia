using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using TMPro;

// by eb
// this script handles any menu behaviour while in the levels
public class PauseMenuManager : MonoBehaviour
{
    [Header("UI Status Items")]
    public GameObject turnCounter;
    public GameObject playerStatus;

    [Header("UI Pause Items")]
    public GameObject pauseButton;

    public GameObject pauseMenu;
    public TMP_Text pauseTitleLabel;
    public GameObject pausePreviousLevelButton;
    public GameObject pauseNextLevelButton;

    [Header("UI Victory Items")]
    public GameObject victoryMenu;
    public TMP_Text victoryTitleLabel;
    public GameObject victoryPreviousLevelButton;
    public GameObject victoryNextLevelButton;
    public GameObject star1, star2, star3;

    // scene build indexes
    private int firstLevelSceneIndex = 1;
    private int lastLevelSceneIndex = 1;

    private bool gameIsPaused = false;

    void Start()
    {
        InitializeLevelUI();
    }

    void Update()
    {
        // use 'esc' key to pause/resume the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if game is playing, pause the game
            // otherwise, resume playing the game
            if (!gameIsPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    // initializes starting state of ui for the level
    private void InitializeLevelUI()
    {
        // at start of scene,
        // turn on level ui
        turnCounter.SetActive(true);
        playerStatus.SetActive(true);
        pauseButton.SetActive(true);

        // set title of level text
        pauseTitleLabel.text = "Level " + SceneManager.GetActiveScene().buildIndex;
        victoryTitleLabel.text = "Level " + SceneManager.GetActiveScene().buildIndex;

        // check if the scene has a previous and next scene and disable buttons accordingly
        bool hasPreviousLevel = HasPreviousLevel();
        bool hasNextLevel = HasNextLevel();
        pausePreviousLevelButton.SetActive(hasPreviousLevel);
        pauseNextLevelButton.SetActive(hasNextLevel);
        victoryPreviousLevelButton.SetActive(hasPreviousLevel);
        victoryNextLevelButton.SetActive(hasNextLevel);

        // turn off pause menu
        pauseMenu.SetActive(false);
        gameIsPaused = false;

        // turn off victory menu
        victoryMenu.SetActive(false);
    }

    // returns false if the current level does not have a previous level
    // otherwise, returns true
    private bool HasPreviousLevel()
    {
        Debug.Log("Checking for previous level...");

        // get the current scene's build index
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        // check if this scene is the first level
        if (sceneIndex == firstLevelSceneIndex)
        {
            Debug.Log("No previous level found.");
            return false;
        }

        Debug.Log("Previous level found.");
        return true;
    }

    // returns false if the current level does not have a next level
    // otherwise, returns true
    private bool HasNextLevel()
    {
        Debug.Log("Checking for next level...");

        // get the current scene's build index
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        // check if this scene is the last level
        if (sceneIndex == lastLevelSceneIndex)
        {
            Debug.Log("No next level found.");
            return false;
        }

        Debug.Log("Next level found.");
        return true;
    }

    // opens the victory screen
    // displays number of stars earned on level complete
    public void GoToVictoryScreen(int stars)
    {
        // make sure the pause menu is closed
        if (gameIsPaused)
        {
            ResumeGame();
        }

        // make sure pause button is off
        pauseButton.SetActive(false);

        // open the victory menu
        victoryMenu.SetActive(true);

        Debug.Log("Victory! " + stars + " star(s) earned.");
        // set number of stars
        // CHANGE TO SPRITE SWAP LATER
        switch (stars)
        {
            case 0:
                victoryNextLevelButton.SetActive(false);
                star1.SetActive(false);
                star2.SetActive(false);
                star3.SetActive(false);
                break;
            case 1:
                star1.SetActive(true);
                star2.SetActive(false);
                star3.SetActive(false);
                break;
            case 2:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(false);
                break;
            case 3:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                break;
            default:
                Debug.Log("Number of stars not recieved for victory screen. Defaulting to zero stars.");
                victoryNextLevelButton.SetActive(false);
                star1.SetActive(false);
                star2.SetActive(false);
                star3.SetActive(false);
                break;
        }
    }

    #region Button Functions

    // pauses game
    public void PauseGame()
    {
        Debug.Log("Pausing game...");

        // open the pause menu
        pauseMenu.SetActive(true);
        gameIsPaused = true;

        // turn off the pause button
        pauseButton.SetActive(false);

        // pause the scene
        Time.timeScale = 0f;
    }

    // resumes game
    public void ResumeGame()
    {
        Debug.Log("Resuming game...");

        // close the pause menu
        pauseMenu.SetActive(false);
        gameIsPaused = false;

        // turn on the pause button
        pauseButton.SetActive(true);

        // resume the scene
        Time.timeScale = 1f;
    }

    // go to previous level
    public void GoToPreviousLevel()
    {
        Debug.Log("Going to the previous level...");

        // get the current scene's build index
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        // go to the previous scene
        SceneManager.LoadScene(sceneIndex - 1);
    }

    // go to next level
    public void GoToNextLevel()
    {
        Debug.Log("Going to the next level...");

        // get the current scene's build index
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        // go to the next scene
        SceneManager.LoadScene(sceneIndex + 1);
    }

    // restarts the level when restart button is pressed
    public void RestartLevel()
    {
        Debug.Log("Restarting Level...");

        // get the current scene's build index
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        // reload the scene
        SceneManager.LoadScene(sceneIndex);
    }

    // returns to the main menu scene when exit button is pressed
    public void ReturnToMainMenu()
    {
        Debug.Log("Returning to Main Menu Scene...");

        SceneManager.LoadScene("Eb_MainMenu");
    }

    #endregion
}
