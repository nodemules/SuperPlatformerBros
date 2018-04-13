using System;
using UnityEngine;

namespace UserInterface.Menu
{
    public class MainMenuController : MonoBehaviour
    {

        public void StartGame()
        {
            GlobalGameState.RestartGame();
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit ();
#endif
        }

        public void ResumeGame()
        {
            GetComponentInParent<PauseMenu>().Hide();
        }
        
    }
}