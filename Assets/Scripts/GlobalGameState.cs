using System;
using System.Collections;
using System.Collections.Generic;
using Environment;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameState : Singleton<GlobalGameState>
{
    private const int MaxLives = 3;

    private static GlobalGameState _instance;
    public static string CurrentLevel { get; set; }

    public static int Lives { get; private set; }
    public static int Coins { get; set; }

    public static List<Vector3> CollectedCoinPositions { get; private set; }

    private static void SetDefaults()
    {
        CollectedCoinPositions = new List<Vector3>();
        CurrentLevel = LevelLoader.FirstLevel;
        Lives = MaxLives;
        Coins = 0;
    }

    public void Start()
    {
        print("GlobalGameState.Start()");
        SetDefaults();
        if (CurrentLevel == null)
        {
            CurrentLevel = LevelLoader.FirstLevel;
            SceneManager.LoadScene(CurrentLevel);
            return;
        }

        print("Changing level to: " + CurrentLevel);
        LevelLoader.ChangeLevel(CurrentLevel);
    }

    public static void PlayerDeath()
    {
        print("Player has died");
        Lives--;
        if (Lives == 0)
        {
            SetDefaults();
            ApplicationState.Ending = -1;
            print("Player has run out of lives, Game over");
            SceneManager.LoadScene("GameOver");
            return;
        }

        print(Lives + " Lives remaining");
        print("Restarting" + CurrentLevel);
        LevelLoader.ChangeLevel(CurrentLevel);
    }

    public static void CollectCoin(GameObject coin)
    {
        CollectedCoinPositions.Add(coin.transform.position);
        UICoinController.CollectCoin();
    }
    
    public static void EnableSceneListener()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
         
    public static void DisableSceneListener()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
         
    private static void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        GameObject[] rootGameObjects = scene.GetRootGameObjects();
        DisableCollectedCoins(rootGameObjects);
        DisableSceneListener();
    }

    private static void DisableCollectedCoins(IEnumerable<GameObject> sceneGameObjects)
    {

        List<Coin> coins = new List<Coin>();
        foreach (GameObject gameObject in sceneGameObjects) 
        {
            coins.AddRange(gameObject.GetComponentsInChildren<Coin>());
        }

        foreach (Coin coin in coins)
        {
            GameObject coinGameObject = coin.transform.gameObject;
            foreach (Vector3 position in CollectedCoinPositions)
            {
                if (position == coinGameObject.transform.position)
                {
                    Destroy(coinGameObject); 
                }
            }
        }
    }
}