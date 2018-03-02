using Interfaces;
using UnityEngine;

namespace Environment
{
    public class HideableBlock : Block, IHideable
    {
        private BoxCollider2D _boxCollider;
        public bool Initialized { get; set; }
        public SpriteRenderer SpriteRenderer { get; set; }
        [SerializeField] private bool _startVisible;
        public bool StartVisible
        {
            get { return _startVisible; }
            set { _startVisible = value; }
        }

        public void Start()
        {
            if (!Initialized)
            {
                DoInitialization();
            }

            if (StartVisible)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public void DoInitialization()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Initialized = true;
        }

        public void Hide()
        {
            if (!Initialized)
            {
                DoInitialization();
            }

            _boxCollider.enabled = false;
            SpriteRenderer.enabled = false;
        }

        public void Show()
        {
            if (!Initialized)
            {
                DoInitialization();
            }

            _boxCollider.enabled = true;
            SpriteRenderer.enabled = true;
        }
    }
}