using Interfaces;
using UnityEngine;

namespace Environment
{
    public class ActionBlock : Block, ITrigger
    {
        [SerializeField] private int _numTriggers = 1;
        [SerializeField] private GameObject _target;

        public int TriggerCount { get; set; }

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

        public ITriggerable Triggerable { get; set; }

        public void Start()
        {
            print("Target: " + Target);
            if (Target != null)
            {
                Triggerable = Target.GetComponent<ITriggerable>();
                print("Trigger found: " + Triggerable);
            }
        }

        public new void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
            Player.Player player = other.collider.GetComponent<Player.Player>();
            if (player != null)
            {
                if (Triggerable != null && TriggerCount < NumTriggers)
                {
                    TriggerCount++;
                    Triggerable.Trigger();
                }
            }
        }
    }
}