using System;
using UnityEngine;

namespace UserInterface.Menu
{
    public class MainMenuController : MonoBehaviour
    {
        public void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                GlobalGameState.RestartGame();
            }

            if (Input.GetButtonDown("Cancel"))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit ();
#endif
            }
        }
    }
}