using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    private int _ending;

    private GameObject _endingText;

    public void Start()
    {
        GameObject gameOverCamera = GameObject.Find("GameOverCamera");
        Camera camera = gameOverCamera.GetComponent<Camera>();
        camera.enabled = true;
        _ending = ApplicationState.Ending;
        RunEnding();
    }

    private void RunEnding()
    {
        switch (_ending)
        {
            case -1:
                _endingText = GameObject.Find("YouDeadText");
                break;
            case 1:
                _endingText = GameObject.Find("YouWinText");
                break;
            default:
//                RestartGame();
                break;
        }

        if (_endingText == null)
        {
//            RestartGame();
            return;
        }


        _endingText.SetActive(true);
    }

    private static void RestartGame()
    {
        ApplicationState.Ending = 1;
        SceneManager.LoadScene("Main");
    }
}