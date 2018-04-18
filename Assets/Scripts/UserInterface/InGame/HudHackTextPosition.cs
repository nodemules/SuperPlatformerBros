using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.InGame
{
    public class HudHackTextPosition : MonoBehaviour
    {
        private RectTransform _rectTransform;
        public float posX;
        public float posY;

        private void Start()
        {
            _rectTransform = gameObject.GetComponent<RectTransform>();
            _rectTransform.anchoredPosition = new Vector2(posX, posY);
        }
    }
}