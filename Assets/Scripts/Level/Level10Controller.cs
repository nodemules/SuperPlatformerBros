using System;
using System.Collections.Generic;
using Foe;
using PlayerCharacter;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Level
{
    public class Level10Controller : MonoBehaviour
    {
        private const string SceneName = "Level10";
        public Text UIText;
        public string TextToType;
        public float TimeToType;

        private float textPercentage;
        private List<IBoss> _bosses = new List<IBoss>();
        private bool _bossesCleared;
        private Scene _scene;
        private bool _powerUpLastBoss;
        public Player Player;

        public void Start()
        {
            _scene = SceneManager.GetActiveScene();
            Invoke("StartFight", 20);
        }

        public void Update()
        {
            if (_scene.name != SceneName || _bossesCleared)
            {
                return;
            }

            IntroSpeech();

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

        private void IntroSpeech()
        {
            int numberOfLettersToShow = (int) (TextToType.Length * textPercentage);
            UIText.text = TextToType.Substring(0, numberOfLettersToShow);
            textPercentage += Time.deltaTime / TimeToType;
            textPercentage = Mathf.Min(1.0f, textPercentage);
        }

        private void StartFight()
        {
            _bosses = new List<IBoss>(GetComponentsInChildren<IBoss>());
            foreach (IBoss boss in _bosses)
            {
                boss.StartMoving();
            }

            PlayerMovement movement = Player.GetComponent<PlayerMovement>();
            movement.MovementEnabled = true;
            
            Invoke("RemoveText", 2);
        }

        private void WinGame()
        {
            ApplicationState.Ending = -100;
            SceneManager.LoadScene("GameOver");
        }

        private void RemoveText()
        {
            UIText.enabled = false;
        }
    }
}