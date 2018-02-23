using UnityEngine;

namespace Environment
{
    public class HiddenBlock : Block
    {
        private BoxCollider2D _boxCollider;
        private SpriteRenderer _spriteRenderer;
        
        public void Start()
        {
            
            _boxCollider = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Hide();
        }

        public void Hide()
        {
            
            _boxCollider.enabled = false;
            _spriteRenderer.enabled = false;
        }
        
        public void Show()
        {
            _boxCollider.enabled = true;
            _spriteRenderer.enabled = true;
        }
    }
}