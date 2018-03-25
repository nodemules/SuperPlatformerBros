using System;
using Interfaces;
using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerAttack : MonoBehaviour
    {
        public float AttackDuration = 1.0f;
        public AudioClip AttackAudio;
        
        private BoxCollider2D _weaponBoxCollider2D;
        private SpriteRenderer _weaponSpriteRenderer;
        private Animator _weaponAnimator;
        private AudioSource _attackAudioSource;

        private Player _player;
        private bool _isAttacking;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (GlobalGameState.IsPaused)
            {
                return;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IKillable killable = other.gameObject.GetComponent<IKillable>();
            if (killable != null)
            {
                killable.Kill();
            }
        }

        private void Attack()
        {
            if (_player.IsDead || _isAttacking)
            {
                return;
            }

            _isAttacking = true;
            _attackAudioSource.PlayOneShot(AttackAudio);
            _weaponBoxCollider2D.enabled = true;
            _weaponSpriteRenderer.enabled = true;
            _weaponAnimator.enabled = true;   
            
            Invoke("ResetAttack", AttackDuration);
        }
        
        private void ResetAttack()
        {
            _isAttacking = false;
            _weaponBoxCollider2D.enabled = false;
            _weaponSpriteRenderer.enabled = false;
            _weaponAnimator.enabled = false;
        }
        
        private void Initialize()
        {
            _attackAudioSource = GetComponent<AudioSource>();
            
            _weaponBoxCollider2D = gameObject.GetComponent<BoxCollider2D>();
            _weaponSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _weaponAnimator = gameObject.GetComponent<Animator>();

            _player = gameObject.GetComponentInParent<Player>();
            print("Player found: " + _player);
            
            _weaponSpriteRenderer.enabled = false;
            _weaponBoxCollider2D.enabled = false;
            _weaponAnimator.enabled = false;
        }
    }
}