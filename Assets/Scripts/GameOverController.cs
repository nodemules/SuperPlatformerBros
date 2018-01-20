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

    private GameObject _endingText;
    private Camera _camera;
    private GameObject _restartText;

    private void Start()
    {
        _restartText = GameObject.Find("RestartText");
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
        print("Toggling cameras!");
        GameObject gameOverCamera = GameObject.Find("GameOverCamera");
        _camera = gameOverCamera.GetComponent<Camera>();
        print("_camera=" + _camera.name);
        _camera.enabled = true;
    }

    private void RunEnding()
    {
        ToggleTimescale();
        print("Running ending script, _ending=" + _ending);
        switch (_ending)
        {
            case -1:
                _endingText = GameObject.Find("YouDeadText");
                break;
            case 1:
                _endingText = GameObject.Find("YouWinText");
                break;
            default:
                RestartGame();
                break;
        }

        if (_endingText != null)
        {
            print("_endingText=" + _endingText.name);
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
        ToggleTimescale();
        ApplicationState.Ending = 0;
        _endingActive = false;
        SceneManager.LoadScene("Main");
    }

    private static void ToggleTimescale()
    {
        if (true) return;
        print("Time.timeScale=" + Time.timeScale);
        if (!Equals(Time.timeScale, 0.0f))
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
}