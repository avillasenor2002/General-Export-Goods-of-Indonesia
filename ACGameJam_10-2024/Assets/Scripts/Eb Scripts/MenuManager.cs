using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// by eb
// this script handles all of the menu's buttons and interactions
public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuBG;
    public GameObject levelMenuBG;
    public GameObject settingsMenuBG;
    public GameObject howToMenuBG;
    public GameObject creditsMenuBG;

    private void Start()
    {
        // at start of the scene,
        // turn on main menu screen
        mainMenuBG.SetActive(true);
        // turn off all other screens
        levelMenuBG.SetActive(false);
        howToMenuBG.SetActive(false);
        settingsMenuBG.SetActive(false);
        creditsMenuBG.SetActive(false);
    }

    #region Button Functions

    // goes to the level select screen when LevelButton is pressed
    public void GoToLevels()
    {
        Debug.Log("Going to Levels...");

        // turn off main menu
        TurnOffMainMenu();

        // turn on level select menu
        levelMenuBG.SetActive(true);
    }

    // goes to the how to play screen when HowToButton is pressed
    public void GoToHowTo()
    {
        Debug.Log("Going to How To Play...");

        // turn off main menu
        TurnOffMainMenu();

        // turn on how to play menu
        howToMenuBG.SetActive(true);
    }

    // goes to the settings screen when SettingsButton is pressed
    public void GoToSettings()
    {
        Debug.Log("Going to Settings...");

        // turn off main menu
        TurnOffMainMenu();

        // turn on how to play menu
        settingsMenuBG.SetActive(true);
    }

    // goes to the credits screen when CreditsButton is pressed
    public void GoToCredits()
    {
        Debug.Log("Going to Credits...");

        // turn off main menu
        TurnOffMainMenu();

        // turn on credits menu
        creditsMenuBG.SetActive(true);
    }

    // quits the game when QuitButton is pressed
    public void GoToQuit()
    {
        Debug.Log("Quitting Game...");

        // quit the application
        Application.Quit();
    }

    // goes back to the main menu screen when the BackButton is pressed
    public void GoToMainMenu(GameObject currentScreen)
    {
        Debug.Log("Going to Main Menu...");

        // turn off current screen
        currentScreen.SetActive(false);

        // turn on main menu
        mainMenuBG.SetActive(true);
    }

    #endregion

    // makes main menu invisible
    private void TurnOffMainMenu()
    {
        mainMenuBG.SetActive(false);
    }
}
