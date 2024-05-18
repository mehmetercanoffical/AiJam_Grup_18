using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Load or Show Settings")]
    public GameObject LoadingBar;

    [Header("Test or Edit Settings")]
    public int currentTest = 3;


    [Header("Settings")]
    private int _currentLevel;
    private Scene _lastLoadedScene;
    private const string _level = "Level ";
    public static event UnityAction<bool> OnLevelLoaded;



    private void Awake() => LevelLoad();

    private void LevelLoad()
    {
        _currentLevel = 1;

         SceneLoader(currentTest.ToString());

    }



    void SceneLoader(string sceneName)
    {

        if (LoadingBar != null) LoadingBar.SetActive(true);

        StartCoroutine(SceneController(_level + sceneName));
    }
  

    IEnumerator SceneController(string sceneName)
    {
        OnLevelLoaded?.Invoke(false);

        if (_lastLoadedScene.IsValid())
        {
            SceneManager.UnloadSceneAsync(_lastLoadedScene);

            bool isUnloadScene = false;

            while (!isUnloadScene)
            {
                isUnloadScene = !_lastLoadedScene.IsValid();
                yield return new WaitForEndOfFrame();

            }
        }

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        bool isSceneLoaded = false;

        while (!isSceneLoaded)
        {
            _lastLoadedScene = SceneManager.GetSceneByName(sceneName);
            isSceneLoaded = _lastLoadedScene != null && _lastLoadedScene.isLoaded;
            yield return new WaitForEndOfFrame();
        }


        OnLevelLoaded?.Invoke(true);
        LoadingBar.SetActive(false);

    }

    public void NextLevel() => SetCurrentLevel();
    public void RestartLevel() => LevelLoad();



    public void SetCurrentLevel()
    {
        _currentLevel++;

        if (_currentLevel >= SceneManager.sceneCountInBuildSettings)
            _currentLevel = 1;

        SceneLoader(_currentLevel.ToString());
    }

}




#if UNITY_EDITOR

[CustomEditor(typeof(LevelManager))]
public class LevelManagerCustom : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Next Level"))
        {
            LevelManager.Instance.NextLevel();
        }
    }
}
#endif