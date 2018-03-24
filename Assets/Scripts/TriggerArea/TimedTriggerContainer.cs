using System.Collections.Generic;
using Environment;
using Extensions;
using Interfaces;
using UnityEngine;

namespace TriggerArea
{
    public class TimedTriggerContainer : ExtendedMonoBehavior, ITriggerable
    {
        private ITriggerable[] _triggerables;
        public bool InitialDelay;
        public bool DelayEachTrigger;
        public float Delay;

        public void Start()
        {
            Invoke("DoInitialization", 0.1f);
        }

        private void DoInitialization()
        {
            _triggerables = GetComponentsInChildrenOnly<ITriggerable>();
        }

        public void Trigger()
        {
            if (DelayEachTrigger)
            {
                FireEachTrigger();
            }
            else
            {
                Wait(Delay, FireAllTriggers);
            }
        }

        private void FireAllTriggers()
        {
            foreach (ITriggerable triggerable in _triggerables)
            {
                triggerable.Trigger();
            }
        }

        private void FireEachTrigger()
        {
            int initial = 0;
            if (InitialDelay)
            {
                initial = 1;
            }
            for (int i = 0; i < _triggerables.Length; i++)
            {
                ITriggerable triggerable = _triggerables[i];
                DelayedTrigger(triggerable, Delay * (i + initial));
            }
        }

        private void DelayedTrigger(ITriggerable triggerable, float delay)
        {
            Wait(delay, triggerable.Trigger);
        }
    }
}