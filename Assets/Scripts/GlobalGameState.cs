using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameState : Singleton<GlobalGameState>
{
    private const int MaxLives = 3;

    private static GlobalGameState _instance;
    public static string CurrentLevel { get; set; }

    public static int Lives { get; private set; }

    private static void SetDefaults()
    {
        CurrentLevel = LevelLoader.FirstLevel;
        Lives = MaxLives;
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
            Lives = MaxLives;
            ApplicationState.Ending = -1;
            print("Player has run out of lives, Game over");
            SceneManager.LoadScene("GameOver");
            return;
        }

        print(Lives + " Lives remaining");
        print("Restarting" + CurrentLevel);
        LevelLoader.ChangeLevel(CurrentLevel);
    }
    
}