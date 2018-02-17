using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    private const int NoEnding = 0;
    private int _ending;
    private bool _endingActive;

    private Camera _camera;

    private GameObject _endingText;
    private GameObject _restartText;
    private GameObject _coinText;

    private void Start()
    {
        _restartText = GameObject.Find("RestartText");
        _coinText = GameObject.Find("GameOverCoinCount");
    }

    private void Update()
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
                break;
            case -100:
                _coinText.SetActive(true);
                _endingText = GameObject.Find("YouWinText");
                break;
            default:
                RestartGame();
                break;
        }

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