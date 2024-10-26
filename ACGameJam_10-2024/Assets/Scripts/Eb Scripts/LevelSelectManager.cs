using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// by eb
// this script manages the level selector
public class LevelSelectManager : MonoBehaviour
{
    public void GoToLevel(string sceneName)
    {
        Debug.Log("Going to scene " + sceneName + "...");

        SceneManager.LoadScene(sceneName);
    }
}
