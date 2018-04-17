using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.InGame
{
    public class HudLivesController : MonoBehaviour
    {
        private TextMeshProUGUI _lifeText;
        private int _oldLifeCount;
        private static float _defaultFontSize = 18.0f;
        
        public void Start()
        {
            _lifeText = GetComponentInChildren<TextMeshProUGUI>();
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
            _lifeText.fontSize = 24.0f;
            Invoke("ResetFontSize", 1);
        }

        private void ResetFontSize()
        {
            _lifeText.fontSize = _defaultFontSize;
        }

    }
}