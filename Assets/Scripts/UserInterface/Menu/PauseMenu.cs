using System;
using Interfaces;
using UnityEngine;

// @brenthaertlein: intentionally disabling RedudantJumpStatement
// to ensure only one set of actions per button press is called
// ReSharper disable RedundantJumpStatement

namespace UserInterface.Menu
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

        private static void PauseGame()
        {
            print("Pausing game");
            GlobalGameState.PauseGame();
        }

        public void Show()
        {
            PauseGame();
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            GlobalGameState.ResumeGame();
        }
    }
}