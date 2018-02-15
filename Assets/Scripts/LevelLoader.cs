using System;
using Interfaces;
using TriggerArea;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public StartArea StartArea;
    
    private static readonly string[] Levels = {"Level1", "Level2", "Level3", "Level4", "Level5"};
    private static readonly int HighestLevel = Levels.Length;
    public static readonly string FirstLevel = Levels[0];

    public static void ChangeLevel(string levelName)
    {
        int level = Array.IndexOf(Levels, levelName);
        if (level == -1)
        {
            print("Can't change level to ");
        }
        print("Changing level to Level=" + levelName);
        GlobalGameState.CurrentLevel = levelName;
        GlobalGameState.EnableSceneListener();
        SceneManager.LoadScene(levelName);
    }

    private static void MovePlayerToSceneStartArea(GameObject player, string levelName)
    {
        
        GameObject levelContainer = GameObject.Find(levelName + "Container");
        StartArea startArea = levelContainer.GetComponentInChildren<IBoundary>() as StartArea;
        if (startArea != null)
        {
            print("startArea found!");
            Collider2D startAreaCollider2D = startArea.GetComponent<Collider2D>();
            player.transform.position = startAreaCollider2D.bounds.center;
        }
    }

    public static void NextLevel()
    {
        int level = Array.IndexOf(Levels, GlobalGameState.CurrentLevel) + 1;
        if (level < HighestLevel)
        {
            int nextLevel = level + 1;
            string levelName = Levels[nextLevel - 1];
            ChangeLevel(levelName);
            return;
        }

        if (level == HighestLevel)
        {
            ApplicationState.Ending = -100;
            SceneManager.LoadScene("GameOver");
        }
    }
    
    public static void Restart()
    {
        print("Restarting!");
        ChangeLevel(FirstLevel);
    }
}