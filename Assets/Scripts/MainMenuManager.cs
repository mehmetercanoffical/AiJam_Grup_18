using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private const string TimePointsKey = "TimePoints";
    public void StartNewGame()
    {
        // Reset only the timePoints PlayerPrefs
        PlayerPrefs.DeleteKey(TimePointsKey);
        PlayerPrefs.Save();

        // Get the index of the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene (current scene index + 1)
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
