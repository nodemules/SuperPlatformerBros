using Interfaces;
using UnityEngine;

// @brenthaertlein: intentionally disabling RedudantJumpStatement
// to ensure only one set of actions per button press is called
// ReSharper disable RedundantJumpStatement

namespace System
{
    public class PauseMenu : MonoBehaviour, IUserInterfaceSystem, IHideable
    {
        public bool Initialized { get; set; }
        public SpriteRenderer SpriteRenderer { get; set; }

        public void Start()
        {
            DoInitialization();
        }

        public void DoInitialization()
        {
            Initialized = true;
        }

        public void Update()
        {
            if (GlobalGameState.IsPaused)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ResumeGame();
                    GlobalGameState.LoadMainMenu();
                    return;
                }

                if (Input.GetButtonDown("Jump"))
                {
                    Hide();
                }
            }
        }

        private static void PauseGame()
        {
            print("Pausing game");
            GlobalGameState.PauseGame();
        }

        private static void ResumeGame()
        {
            print("Resuming game");
            GlobalGameState.ResumeGame();
        }

        public void Show()
        {
            PauseGame();
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            ResumeGame();
            gameObject.SetActive(false);
        }
    }
}