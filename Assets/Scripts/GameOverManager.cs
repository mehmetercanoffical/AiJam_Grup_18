using System.Collections;
using System.Collections.Generic;using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    
    private const string TimePointsKey = "TimePoints";
    public void RestartGame()
    {
        SceneManager.LoadScene(0);

        // Delete all timepoints PlayerPrefs when the game is over
        
        PlayerPrefs.DeleteKey(TimePointsKey);
    }

    public void QuitGame()
    {
        // Quit the application (only works in standalone builds)
        Application.Quit();
    }
}