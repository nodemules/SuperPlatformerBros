using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController _gameState;
    private static IPlayer _player;
    private static GameObject _playerGameObject;
    private static string _currentLevel;

    public IPlayer Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private void Awake()
    {
        if (_gameState == null)
        {
            DontDestroyOnLoad(gameObject);
            _gameState = this;
        }
        else
        {
            if (_gameState != this)
            {
                Destroy(_gameState);
            }
        }
    }

    public void Start()
    {
        if (_currentLevel == null)
        {
            _currentLevel = "Level1";
            SceneManager.LoadScene(_currentLevel);
            return;
        }

        print("Changing level to: " + _currentLevel);
//        LevelController.ChangeLevel(_currentLevel);
    }

    public static void SavePlayer(GameObject player)
    {
        _playerGameObject = player;
    }

    public static void SavePlayer(IPlayer player)
    {
        print("Saving player");
        _player = player;
    }

    public static GameObject LoadPlayer()
    {
        print("_playerGameObject=" + _playerGameObject.name);
        return _playerGameObject;
    }
}