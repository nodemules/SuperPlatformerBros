using Enemy;
using Interfaces;
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
            Initialize();
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
            IKillable killable = other.gameObject.GetComponent<IKillable>();
            if (killable != null)
            {
                killable.Kill();
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
        
        private void Initialize()
        {
            _attackAudioSource = GetComponent<AudioSource>();
            
            _weaponBoxCollider2D = gameObject.GetComponent<BoxCollider2D>();
            _weaponSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _weaponAnimator = gameObject.GetComponent<Animator>();
            
            _weaponSpriteRenderer.enabled = false;
            _weaponBoxCollider2D.enabled = false;
            _weaponAnimator.enabled = false;
        }
    }
}