using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

// by eb
// this script handles any menu behaviour while in the levels
public class PauseMenuManager : MonoBehaviour
{
    public GameObject turnCounter;
    public GameObject playerStatus;
    public GameObject pauseButton;

    public GameObject pauseMenu;
    public GameObject previousLevelButton;
    public GameObject nextLevelButton;
    public int firstLevelSceneIndex = 1;
    public int lastLevelSceneIndex = 1;

    private bool gameIsPaused = false;

    void Start()
    {
        // at start of scene,
        // turn on level ui
        turnCounter.SetActive(true);
        playerStatus.SetActive(true);
        pauseButton.SetActive(true);

        // check if the scene has a previous and next scene and disable buttons accordingly
        previousLevelButton.SetActive(HasPreviousLevel());
        nextLevelButton.SetActive(HasNextLevel());

        // turn off pause menu
        pauseMenu.SetActive(false);
        gameIsPaused = false;
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

    // opens the settings menu
    // might change this to just have settings items in the pause menu
    public void OpenSettings()
    {
        Debug.Log("Opening Settings...");
    }

    // returns to the main menu scene when exit button is pressed
    public void ReturnToMainMenu()
    {
        Debug.Log("Returning to Main Menu Scene...");

        SceneManager.LoadScene("Eb_MainMenu");
    }

    #endregion
}
