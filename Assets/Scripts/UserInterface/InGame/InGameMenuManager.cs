﻿using System;
using UnityEngine;
using UserInterface.Menu;

// @brenthaertlein: intentionally disabling RedudantJumpStatement
// to ensure only one set of actions per button press is called
// ReSharper disable RedundantJumpStatement

namespace UserInterface.InGame
{
    public class InGameMenuManager : MonoBehaviour
    {
        private PauseMenu _pauseMenu;

        public void Start()
        {
            _pauseMenu = GetComponentInChildren<PauseMenu>(true);
            print("PauseMenu=" + _pauseMenu);
        }

        private void Update()
        {
            if (!GlobalGameState.IsPaused && Input.GetButtonDown("Cancel"))
            {
                print("Escape pressed while in game, pausing");
                _pauseMenu.Show();
                return;
            }
        }
    }
}