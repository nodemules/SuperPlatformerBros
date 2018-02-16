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
    public static int Coins { get; private set; }

    public static Dictionary<string, List<Vector3>> CollectedCoinPositionsMap { get; private set; }

    private static void SetDefaults()
    {
        CollectedCoinPositionsMap = new Dictionary<string, List<Vector3>>();
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
        Scene scene = SceneManager.GetActiveScene();
        if (CollectedCoinPositionsMap.ContainsKey(scene.name))
        {
            List<Vector3> sceneCollectedCoins = CollectedCoinPositionsMap[scene.name];
            sceneCollectedCoins.Add(coin.transform.position);
        }
        else
        {
            List<Vector3> list = new List<Vector3> {coin.transform.position};
            CollectedCoinPositionsMap.Add(scene.name, list);
        }
        Coins++;
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
        DisableCollectedCoins(scene);
        DisableSceneListener();
    }

    private static void DisableCollectedCoins(Scene scene)
    {

        List<Coin> coins = GetCoinsInScene(scene);

        foreach (Coin coin in coins)
        {
            GameObject coinGameObject = coin.transform.gameObject;
            List<Vector3> coinPositions = CollectedCoinPositionsMap[scene.name];
            foreach (Vector3 position in coinPositions)
            {
                if (position == coinGameObject.transform.position)
                {
                    Destroy(coinGameObject); 
                }
            }
        }
    }

    private static List<Coin> GetCoinsInScene(Scene scene)
    {
        List<Coin> coins = new List<Coin>();
        foreach (GameObject gameObject in scene.GetRootGameObjects()) 
        {
            coins.AddRange(gameObject.GetComponentsInChildren<Coin>());
        }

        return coins;
    }
}