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
            if (player != null)
            {
                if (Triggerable != null && TriggerCount < NumTriggers)
                {
                    TriggerCount++;
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

        public int TriggerCount { get; set; }
        public ITriggerable Triggerable { get; set; }

        public int NumTriggers
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

        #endregion properties
    }
}