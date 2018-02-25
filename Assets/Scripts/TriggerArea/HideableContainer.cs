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
            _hideables = GetComponentsInChildren<IHideable>();
            if (IsHidden)
            {
                Hide();
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