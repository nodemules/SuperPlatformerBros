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
        private AudioSource _audioSource;

        public AudioClip BlockHitAudioClip;


        private new void Start()
        {
            base.Start();
            _sorryText = GameObject.Find("SorryFakeBlockText");
            _audioSource = GetComponent<AudioSource>();
        }

        public new void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
            Player.Player player = other.collider.GetComponent<Player.Player>();
            if (player != null)
            {
                _audioSource.pitch = 1;
                _audioSource.PlayOneShot(BlockHitAudioClip);
                _sorryText.SetActive(true);
                Text text = _sorryText.GetComponent<Text>();
                text.enabled = true;
                Invoke("DisableText", 2);
            }
        }

        private void DisableText()
        {
            _audioSource.pitch = 0.7f;
            _audioSource.PlayOneShot(BlockHitAudioClip);
            _sorryText.SetActive(false);
        }
    }
}