using System;
using Interfaces;
using PlayerCharacter;
using UnityEngine;

namespace Environment
{
    public class Coin : MonoBehaviour, ICollectable, IHideable
    {
        public AudioClip CoinCollectAudio;
        private AudioSource _audioSource;

        private BoxCollider2D _boxCollider;

        public bool Initialized { get; set; }
        public SpriteRenderer SpriteRenderer { get; set; }

        public void Start()
        {
            if (!Initialized)
            {
                DoInitialization();
            }
            _audioSource = GetComponent<AudioSource>();
        }

        public void DoInitialization()
        {
            Initialized = true;
            _boxCollider = GetComponent<BoxCollider2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Player player = other.GetComponent<IPlayer>() as Player;
            if (player != null && !player.IsDead)
            {
                Collect();
            }
        }

        public void Collect()
        {
            PlaySound();
            Hide();
            GlobalGameState.CollectCoin(gameObject);
            Destroy(gameObject, 1f);
        }

        public void Hide()
        {
            if (!Initialized)
            {
                DoInitialization();
            }
            SpriteRenderer.enabled = false;
            _boxCollider.enabled = false;
        }

        public void Show()
        {
            if (!Initialized)
            {
                DoInitialization();
            }
            SpriteRenderer.enabled = true;
            _boxCollider.enabled = true;
        }

        private void PlaySound()
        {
            _audioSource.pitch = 1.9f;
            _audioSource.PlayOneShot(CoinCollectAudio);
        }
    }
}