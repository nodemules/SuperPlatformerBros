using Interfaces;
using PlayerCharacter;
using UnityEngine;

namespace Environment
{
    public class ActionBlock : Block, ITrigger
    {
        public void Start()
        {
            if (Target != null)
            {
                Triggerable = Target.GetComponent<ITriggerable>();
            }

            AudioSource = GetComponent<AudioSource>();
            if (AudioSource == null)
            {
                AudioSource = new AudioSource();
            }
        }

        public new void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
            Player player = other.collider.GetComponent<Player>();
            if (player != null && !player.IsDead)
            {
                if (Triggerable != null && (IsToggle || TriggerFiredCount < NumberOfTriggers))
                {
                    TriggerFiredCount++;
                    Triggerable.Trigger();
                    AudioSource.pitch = 1;
                    AudioSource.PlayOneShot(TriggerAudioClip);
                }
                else
                {
                    AudioSource.PlayOneShot(DefaultAudioClip);
                }
            }
        }

        #region properties

        protected AudioSource AudioSource;

        public AudioClip TriggerAudioClip;
        public AudioClip DefaultAudioClip;

        public int TriggerFiredCount { get; set; }
        public ITriggerable Triggerable { get; set; }

        public bool IsToggle
        {
            get { return _isToggle; }
            set { _isToggle = value; }
        }

        public int NumberOfTriggers
        {
            get { return _numTriggers; }
            set { _numTriggers = value; }
        }

        public GameObject Target
        {
            get { return _target; }
            set { _target = value; }
        }

        [SerializeField] private int _numTriggers = 1;
        [SerializeField] private GameObject _target;
        [SerializeField] private bool _isToggle;

        #endregion properties
    }
}