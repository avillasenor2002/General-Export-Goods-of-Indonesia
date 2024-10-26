using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// by eb
// this script handles any menu behaviour while in the levels
public class PauseMenuManager : MonoBehaviour
{
    public GameObject turnCounter;
    public GameObject playerStatus;
    public GameObject pauseButton;

    public GameObject pauseMenu;

    private bool gameIsPaused = false;

    void Start()
    {
        // at start of scene,
        // turn on level ui
        turnCounter.SetActive(true);
        playerStatus.SetActive(true);
        pauseButton.SetActive(true);

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

    // restarts the level when restart button is pressed
    public void RestartLevel()
    {
        Debug.Log("Restarting Level...");
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
}
