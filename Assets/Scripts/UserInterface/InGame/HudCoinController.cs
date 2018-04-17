using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.InGame
{
    public class HudCoinController : MonoBehaviour
    {
        private static float _defaultFontSize = 18.0f;
        private TextMeshProUGUI _coinText;
        private GameObject _coinIcon;
        private int _oldCoinCount;

        public void Start()
        {
            _coinText = GetComponentInChildren<TextMeshProUGUI>();
            _coinIcon = GameObject.Find("UICoinIcon");
            _defaultFontSize = _coinText.fontSize;
            _oldCoinCount = GlobalGameState.Coins;
        }

        private void Update()
        {
            if (_oldCoinCount < GlobalGameState.Coins)
            {
                CollectCoinEffect();
            }

            _oldCoinCount = GlobalGameState.Coins;
            _coinText.text = "x " + GlobalGameState.Coins;
            _coinIcon.transform.Rotate(new Vector2(0.0f, 45) * Time.deltaTime);
        }

        private void CollectCoinEffect()
        {
            _coinText.fontSize = 24.0f;
            Invoke("ResetFontSize", 1);
        }

        private void ResetFontSize()
        {
            _coinText.fontSize = _defaultFontSize;
        }
    }
}