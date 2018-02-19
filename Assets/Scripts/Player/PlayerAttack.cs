﻿using System.Runtime.Serialization.Formatters;
using Enemy;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private BoxCollider2D _weaponBoxCollider2D;
        private SpriteRenderer _weaponSpriteRenderer;
        private Animator _weaponAnimator;
        public AudioClip AttackAudio;
        private AudioSource _attackAudioSource;
        
        private void Start()
        {
            
            _weaponBoxCollider2D = gameObject.GetComponent<BoxCollider2D>();
            _weaponBoxCollider2D.enabled = false;
            _weaponSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _weaponSpriteRenderer.enabled = false;
            _weaponAnimator = gameObject.GetComponent<Animator>();
            _weaponAnimator.enabled = false;

            _attackAudioSource = GetComponent<AudioSource>();

        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Enemy.Enemy enemy = other.gameObject.GetComponent<IEnemy>() as Enemy.Enemy;
            if (enemy != null)
            {
                enemy.Kill();
            }
        }

        private void Attack()
        {
            _attackAudioSource.PlayOneShot(AttackAudio);
            _weaponBoxCollider2D.enabled = true;
            _weaponSpriteRenderer.enabled = true;
            _weaponAnimator.enabled = true;   
            
            Invoke("ResetAttack", 1);
        }
        
        private void ResetAttack()
        {
            _weaponBoxCollider2D.enabled = false;
            _weaponSpriteRenderer.enabled = false;
            _weaponAnimator.enabled = false;
        }
        
    }
}