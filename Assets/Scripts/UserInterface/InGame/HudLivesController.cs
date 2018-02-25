using System;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.InGame
{
    public class HudLivesController : MonoBehaviour
    {
        private Text _lifeText;
        
        public void Start()
        {
            _lifeText = GetComponentInChildren<Text>();
        }

        public void Update()
        {
            _lifeText.text = "x " + GlobalGameState.Lives;
        }

    }
}