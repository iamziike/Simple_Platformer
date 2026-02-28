using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Level
{
    public string name { get; private set; }
    public bool isCompleted { get; private set; }
    public int score { get; private set; }
    public float timeTaken { get; private set; }

    public Level(string levelName)
    {
        name = levelName;
        isCompleted = false;
        score = 0;
        timeTaken = 0f;
    }

    public void HandleLevelCompleted(int score, float timeTaken)
    {
        isCompleted = true;
        this.score = score;
        this.timeTaken = timeTaken;
    }
}

[CreateAssetMenu(fileName = "LevelManager", menuName = "ScriptableObjects/LevelManager", order = 1)]
public class LevelManager : ScriptableObject
{
    public Level[] levels { get; private set; }
    public Level currentLevel { get; private set; }

    private void OnEnable()
    {
        if (levels == null || levels.Length == 0)
        {
            string[] levelScenePaths = AssetDatabase.FindAssets("t:Scene", new[] { "Assets/Scenes/Levels" });
            levels = new Level[levelScenePaths.Length];

            for (int i = 0; i < levelScenePaths.Length; i++)
            {
                string scenePath = AssetDatabase.GUIDToAssetPath(levelScenePaths[i]);
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                levels[i] = new Level(sceneName);
            }

            if (levels.Length > 0)
            {
                currentLevel = levels[0];
            }
        }
    }

    public void SetCurrentLevel(string levelName)
    {
        foreach (Level level in levels)
        {
            if (level.name == levelName)
            {
                currentLevel = level;
                return;
            }
        }
        Debug.LogWarning($"Level with name {levelName} not found.");
    }

    public Level GetNextLevel()
    {
        if (currentLevel == null)
        {
            Debug.LogWarning("Current level is not set.");
            return null;
        }

        int currentIndex = System.Array.IndexOf(levels, currentLevel);
        if (currentIndex >= 0 && currentIndex < levels.Length - 1)
        {
            return levels[currentIndex + 1];
        }
        else
        {
            Debug.LogWarning("No more levels available.");
            return null;
        }
    }
}