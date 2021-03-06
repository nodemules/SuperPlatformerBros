﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Menu
{
    public class GameOverController : MonoBehaviour
    {
        private const int NoEnding = 0;
        private int _ending;
        private bool _endingActive;

        private Camera _camera;

        private GameObject _endingText;
        private GameObject _restartText;
        private GameObject _coinText;
        private BackgroundMusicSystem _backgroundMusicSystem;
        public AudioClip GameOverMusic;
        public AudioClip WinningMusic;

        public void Start()
        {
            _restartText = GameObject.Find("RestartText");
            _coinText = GameObject.Find("GameOverCoinCount");
            _backgroundMusicSystem = GetComponentInChildren<BackgroundMusicSystem>();
        }

        public void Update()
        {
            _ending = ApplicationState.Ending;
            if (_ending != NoEnding)
            {
                if (!_endingActive)
                {
                    _endingActive = true;
                    ToggleCameras();
                    RunEnding();
                }

                if (Input.GetButtonDown("Jump"))
                {
                    RestartGame();
                }

                if (Input.GetKey(KeyCode.Escape))
                {
                    GlobalGameState.LoadMainMenu();
                }
            }
        }

        private void ToggleCameras()
        {
            GameObject gameOverCamera = GameObject.Find("GameOverCamera");
            _camera = gameOverCamera.GetComponent<Camera>();
            _camera.enabled = true;
        }

        private void RunEnding()
        {
            switch (_ending)
            {
                case -1:
                    _coinText.SetActive(false);
                    _endingText = GameObject.Find("YouDeadText");
                    _backgroundMusicSystem.BackgroundMusicAudioClip = GameOverMusic;
                    break;
                case -100:
                    _coinText.SetActive(true);
                    _endingText = GameObject.Find("YouWinText");
                    _backgroundMusicSystem.BackgroundMusicAudioClip = WinningMusic;
                    break;
                default:
                    RestartGame();
                    break;
            }
            
            _backgroundMusicSystem.StartBackgroundMusic();

            if (_endingText != null)
            {
                Text text = _endingText.GetComponent<Text>();
                text.enabled = true;
            }

            if (_restartText != null)
            {
                Text text = _restartText.GetComponent<Text>();
                text.enabled = true;
            }
        }

        private void RestartGame()
        {
            ApplicationState.Ending = 0;
            _endingActive = false;
            GlobalGameState.RestartGame();
        }
    }
}