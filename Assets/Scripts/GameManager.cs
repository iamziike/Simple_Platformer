using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    static public GameManager instance { get; private set; }
    public int gameScore { get; private set; }
    public int currentLevelScore { get; private set; }
    [SerializeField] LevelManager levelManager;
    public string currentSceneName { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        currentLevelScore = 0;
        SwitchScene(levelManager.currentLevel.name);
    }

    public void SwitchScene(string sceneName)
    {
        FadeController.instance.FadeOutFadeIn(1f, () =>
        {
            SceneManager.LoadScene(sceneName);
            currentSceneName = sceneName;
        });
    }

    public void ContinueGame()
    {
        LoadLevelsScreen();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSettingsScreen()
    {
        SwitchScene(Constants.Screen.SettingsScreen);
    }

    public void LoadLevelsScreen()
    {
        SwitchScene(Constants.Screen.LevelsScreen);
    }

    public void LoadMainMenu()
    {
        SwitchScene(Constants.Screen.MainMenuScreen);
    }

    public void LoadNextLevel()
    {
        Level level = levelManager.GetNextLevel();
        if (level != null)
        {
            SwitchScene(level.name);
            levelManager.SetCurrentLevel(level.name);
        }
    }
}
