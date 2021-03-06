﻿using System.Collections.Generic;
using Environment;
using Extensions;
using Interfaces;
using Level;
using TriggerArea;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace System
{
    public class GlobalGameState : Singleton<GlobalGameState>
    {
        #region properties

        private const int MaxLives = 3;
        private const int CoinsForExtraLife = 10;
        private const string MainMenuScene = "MainMenu";
        private const string GameOverScene = "GameOver";

        private static GlobalGameState _instance;

        public StartArea StartArea;

        public static string CurrentLevel { get; set; }
        public static bool IsPaused { get; private set; }
        public static int Lives { get; private set; }
        public static int Coins { get; private set; }

        private static Dictionary<string, List<Vector3>> CollectedCoinPositionsMap { get; set; }

        #endregion

        public void Start()
        {
            print("Starting the game");
            SetDefaults();
            if (CurrentLevel == MainMenuScene)
            {
                SceneManager.LoadScene(MainMenuScene);
                return;
            }

            LevelLoader.ChangeLevel(CurrentLevel);
        }

        private static void SetDefaults()
        {
            Time.timeScale = 1f;
            IsPaused = false;
            CollectedCoinPositionsMap = new Dictionary<string, List<Vector3>>();
            CurrentLevel = MainMenuScene;
            Lives = MaxLives;
            Coins = 0;
        }

        public static void RestartGame()
        {
            SetDefaults();
            LevelLoader.ChangeLevel(LevelLoader.FirstLevel);
        }

        public static void PauseGame()
        {
            Time.timeScale = 0f;
            IsPaused = true;
        }

        public static void ResumeGame()
        {
            IsPaused = false;
            Time.timeScale = 1f;
        }

        public static void LoadMainMenu()
        {
            SetDefaults();
            CurrentLevel = MainMenuScene;
            SceneManager.LoadScene(MainMenuScene);
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

        public static void PlayerDeath()
        {
            print("Player has died");
            Lives--;
            if (Lives == 0)
            {
                ApplicationState.Ending = -1;
                print("Player has run out of lives, Game over");
                SceneManager.LoadScene(GameOverScene);
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

            if (Coins % CoinsForExtraLife == 0)
            {
                ExtraLife();
            }
            
        }

        private static void ExtraLife()
        {
            Lives++;
        }

        private static void DisableCollectedCoins(Scene scene)
        {
            if (!CollectedCoinPositionsMap.ContainsKey(scene.name))
            {
                return;
            }

            List<Coin> coins = GetCoinsInScene(scene);
            List<Vector3> coinPositions = CollectedCoinPositionsMap[scene.name];

            if (coins.Count == coinPositions.Count)
            {
                foreach (Coin coin in coins)
                {
                    Destroy(coin.transform.gameObject);
                }

                return;
            }

            foreach (Vector3 position in coinPositions)
            {
                foreach (Coin coin in coins)
                {
                    GameObject coinGameObject = coin.transform.gameObject;
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
    }
}