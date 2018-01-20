using System;
using Interfaces;
using TriggerArea;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static readonly string[] Levels = {"Level1", "Level2"};
    private static int _highestLevel = Levels.Length;
    private static int _currentLevel = 1;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _highestLevel = Levels.Length;
    }

    public static void ChangeLevel(string levelName)
    {
        int level = Array.IndexOf(Levels, levelName);
        _currentLevel = level + 1;
        print("Changing level to Level" + _currentLevel);
        GameObject player = GameObject.Find("Player_0");
        if (player == null)
        {
            print("player=null");
            return;
        }

        print("player=" + player.name);
        DontDestroyOnLoad(player);
        Scene scene = SceneManager.GetSceneByName(levelName);
        SceneManager.LoadScene(scene.name);
        SceneManager.MoveGameObjectToScene(player, scene);
        MovePlayerToSceneStartArea(player, scene.name);
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
}