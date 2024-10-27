using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlexQuit : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("You q!");
        Application.Quit();
    }
}
