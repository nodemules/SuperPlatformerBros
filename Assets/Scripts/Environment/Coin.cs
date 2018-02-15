using Interfaces;
using UnityEngine;

namespace Environment
{
    public class Coin : MonoBehaviour, ICollectable
    {
        public AudioClip CoinCollectAudio;
        private AudioSource _audioSource;

        public void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Player.Player player = other.GetComponent<IPlayer>() as Player.Player;
            if (player != null)
            {
                Collect();
            }
        }

        public void Collect()
        {
            PlaySound();
            Disable();
            GlobalGameState.CollectCoin(gameObject);
            Destroy(gameObject, 1f);
        }

        private void Disable()
        {
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
            boxCollider.enabled = false;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;

        }

        private void PlaySound()
        {
            _audioSource.pitch = 1.9f;
            _audioSource.PlayOneShot(CoinCollectAudio);
        }
    }
}