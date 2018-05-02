using System;
using System.Collections.Generic;
using Foe;
using PlayerCharacter;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Level
{
    public class Level10Controller : MonoBehaviour
    {
        private const string SceneName = "Level10";
        public TextMeshProUGUI UiText;
        public string TextToType;
        public float TimeToType;

        private BackgroundMusicSystem _backgroundMusicSystem;
        public AudioClip FightMusic;

        private float _textPercentage;
        private List<IBoss> _bosses = new List<IBoss>();
        private Scene _scene;
        private bool _bossesCleared;
        private bool _powerUpLastBoss;
        private bool _fightStarted;
        private bool _skippedIntro;
        public Player Player;

        public void Start()
        {
            _scene = SceneManager.GetActiveScene();
            _backgroundMusicSystem = GetComponentInChildren<BackgroundMusicSystem>();

            if (!_fightStarted)
            {
                Invoke("StartFight", 24.5f);
            }
        }

        public void Update()
        {
            if (_scene.name != SceneName || _bossesCleared)
            {
                return;
            }

            if (!_fightStarted && Input.GetButtonDown("Jump"))
            {
                _skippedIntro = true;
                StartFight();
            }

            IntroSpeech();

            _bosses = new List<IBoss>(GetComponentsInChildren<IBoss>());

            if (_bosses.Count == 0)
            {
                _bossesCleared = true;
                Invoke("WinGame", 3);
            }
        }

        private void IntroSpeech()
        {
            if (!_skippedIntro)
            {
                int numberOfLettersToShow = (int) (TextToType.Length * _textPercentage);
                UiText.text = TextToType.Substring(0, numberOfLettersToShow);
                _textPercentage += Time.deltaTime / TimeToType;
                _textPercentage = Mathf.Min(1.0f, _textPercentage);
            }
            else
            {
                UiText.text = "It's very rude to skip a final boss' speech!";
            }
        }

        private void StartFight()
        {
            _fightStarted = true;
            _backgroundMusicSystem.StopBackgroundMusic();
            _backgroundMusicSystem.BackgroundMusicAudioClip = FightMusic;
            _backgroundMusicSystem.StartBackgroundMusic();
            foreach (IBoss boss in _bosses)
            {
                boss.StartMoving();
            }

            PlayerMovement movement = Player.GetComponent<PlayerMovement>();
            movement.MovementEnabled = true;

            Invoke("RemoveText", _skippedIntro ? 3f : 0.5f);
        }

        private void WinGame()
        {
            ApplicationState.Ending = -100;
            SceneManager.LoadScene("WinScreen");
        }

        private void RemoveText()
        {
            UiText.enabled = false;
        }
    }
}