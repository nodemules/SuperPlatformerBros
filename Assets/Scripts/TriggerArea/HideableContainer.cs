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
            print(_hideables.Length + " Hideables found in HideableContainer[" + gameObject.name +
                  "]");
            if (IsHidden)
            {
                Hide();
            }
        }

        private void Show()
        {
            IsHidden = false;
            foreach (IHideable hideable in _hideables)
            {
                hideable.Show();
            }
        }

        private void Hide()
        {
            IsHidden = true;
            foreach (IHideable hideable in _hideables)
            {
                hideable.Hide();
            }
        }

        public void Trigger()
        {
            print("HideableContainer is hidden: " + IsHidden);
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