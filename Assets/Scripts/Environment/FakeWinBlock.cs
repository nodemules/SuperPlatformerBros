using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Environment
{
    public class FakeWinBlock : ActionBlock
    {
        private GameObject _sorryText;
        private float _startTime;


        private new void Start()
        {
            base.Start();
            _sorryText = GameObject.Find("SorryFakeBlockText");
        }

        public new void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
            Player.Player player = other.collider.GetComponent<Player.Player>();
            if (player != null)
            {
                _sorryText.SetActive(true);
                Text text = _sorryText.GetComponent<Text>();
                text.enabled = true;
                Invoke("DisableText", 2);
            }
        }

        private void DisableText()
        {
            AudioSource.pitch = 0.7f;
            AudioSource.PlayOneShot(TriggerAudioClip);
            _sorryText.SetActive(false);
        }
    }
}