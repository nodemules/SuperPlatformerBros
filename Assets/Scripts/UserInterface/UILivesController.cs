using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class UILivesController : MonoBehaviour
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