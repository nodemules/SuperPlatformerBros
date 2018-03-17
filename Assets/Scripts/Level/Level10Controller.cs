using System;
using System.Collections.Generic;
using Environment;
using Foe;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class Level10Controller : MonoBehaviour
    {
        private const string SceneName = "Level10";
        private List<IBoss> _bosses = new List<IBoss>();
        private bool _bossesCleared;
        private Scene _scene;
        private bool _powerUpLastBoss;

        public void Start()
        {
            _scene = SceneManager.GetActiveScene();
        }

        public void Update()
        {
            if (_scene.name != SceneName || _bossesCleared)
            {
                return;
            }

            _bosses = new List<IBoss>(GetComponentsInChildren<IBoss>());
            bool lastBoss = _bosses.Count == 1;
            if (lastBoss && !_powerUpLastBoss)
            {
                IPowerful boss = _bosses[0] as IPowerful;
                if (boss != null)
                {
                    _powerUpLastBoss = true;
                    boss.PowerUp();
                }
            }
            _bossesCleared = _bosses.Count == 0;

            if (_bossesCleared)
            {
                Invoke("WinGame", 3);
            }
        }

        private void WinGame()
        {
            ApplicationState.Ending = -100;
            SceneManager.LoadScene("GameOver");
        }
    }
}