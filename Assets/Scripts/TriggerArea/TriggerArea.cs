using Interfaces;
using PlayerCharacter;
using UnityEngine;

namespace TriggerArea
{
    public class TriggerArea : MonoBehaviour, ITrigger
    {
        #region properties

        [SerializeField] private int _numberOfTriggers;
        [SerializeField] private bool _isToggle;
        [SerializeField] private GameObject _target;

        public bool IsToggle
        {
            get { return _isToggle; }
            set { _isToggle = value; }
        }

        public int NumberOfTriggers
        {
            get { return _numberOfTriggers; }
            set { _numberOfTriggers = value; }
        }

        public GameObject Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public int TriggerFiredCount { get; set; }
        public ITriggerable Triggerable { get; set; }

        #endregion

        public void Start()
        {
            if (Target != null)
            {
                Triggerable = Target.GetComponent<ITriggerable>();
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null && Triggerable != null &&
                (IsToggle || TriggerFiredCount < NumberOfTriggers))
            {
                print("TriggerFiredCount=" + TriggerFiredCount);
                print("NumberOfTriggers=" + NumberOfTriggers);
                print("Firing " + _target.name + ".Trigger()");
                TriggerFiredCount++;
                Triggerable.Trigger();
            }
        }
    }
}