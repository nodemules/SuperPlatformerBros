using System;
using Interfaces;
using TriggerArea;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public StartArea StartArea;
    
    private static readonly string[] Levels = {"Level1", "Level2", "Level3", "Level4", "Level5"};
    private static int _highestLevel = Levels.Length;
    private static int _currentLevel = 1;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _highestLevel = Levels.Length;
//        GameObject levelContainer = GameObject.Find(levelName + "Container");
//        StartArea = levelContainer.GetComponentInChildren<IBoundary>() as StartArea;
    }

    public static void ChangeLevel(string levelName)
    {
        int level = Array.IndexOf(Levels, levelName);
        print("levelName=" + levelName);
        _currentLevel = level + 1;
        print("Changing level to Level" + _currentLevel);
//        GameObject player = GameObject.Find("Player_0");
//        if (player == null)
//        {
//            return;
//        }
//
//        GameController.SavePlayer(player);
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
        if (_currentLevel < _highestLevel)
        {
            int nextLevel = _currentLevel + 1;
            string levelName = Levels[nextLevel - 1];
            ChangeLevel(levelName);
            return;
        }

        if (_currentLevel == _highestLevel)
        {
            ApplicationState.Ending = -100;
            SceneManager.LoadScene("GameOver");
        }
    }
    
    public static void Restart()
    {
        print("Restarting!");
        _currentLevel = 0;
        ChangeLevel("Level1");
    }
}