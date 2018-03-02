using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace TriggerArea
{
    public class HideableContainer : MonoBehaviour, ITriggerable
    {
        public bool IsHidden = true;
        private IHideable[] _hideables;

        public void Start()
        {
            Invoke("DoInitialization", 0.1f);
        }

        private void DoInitialization()
        {
            _hideables = GetComponentsInChildren<IHideable>();
            if (IsHidden)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        private void Show()
        {
            IsHidden = false;
            _hideables = GetComponentsInChildren<IHideable>();
            foreach (IHideable hideable in _hideables)
            {
                hideable.Show();
            }
        }

        private void Hide()
        {
            IsHidden = true;
            _hideables = GetComponentsInChildren<IHideable>();
            foreach (IHideable hideable in _hideables)
            {
                hideable.Hide();
            }
        }

        public void Trigger()
        {
            if (IsHidden)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
    }
}