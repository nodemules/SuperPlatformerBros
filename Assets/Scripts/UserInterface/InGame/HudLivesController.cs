using System;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.InGame
{
    public class HudLivesController : MonoBehaviour
    {
        private Text _lifeText;
        private int _oldLifeCount;
        private static int _defaultFontSize = 18;
        
        public void Start()
        {
            _lifeText = GetComponentInChildren<Text>();
            _oldLifeCount = GlobalGameState.Lives;
        }

        public void Update()
        {
            if (_oldLifeCount < GlobalGameState.Lives)
            {
                ExtraLifeEffect();
            }
            
            _oldLifeCount = GlobalGameState.Lives;
            _lifeText.text = "x " + GlobalGameState.Lives;
        }
        
        private void ExtraLifeEffect()
        {
            _lifeText.fontSize = 24;
            Invoke("ResetFontSize", 1);
        }

        private void ResetFontSize()
        {
            _lifeText.fontSize = _defaultFontSize;
        }

    }
}