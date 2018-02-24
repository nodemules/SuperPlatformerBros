using System.Linq.Expressions;
using Environment;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class UICoinController : MonoBehaviour
    {
        private static int _defaultFontSize = 18;
        private Text _coinText;
        private GameObject _coinIcon;
        private int _oldCoinCount;

        public void Start()
        {
            _coinText = GetComponentInChildren<Text>();
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
            _coinText.fontSize = 24;
            Invoke("ResetFontSize", 1);
        }

        private void ResetFontSize()
        {
            _coinText.fontSize = _defaultFontSize;
        }
    }
}
