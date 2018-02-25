using UnityEngine;

namespace Interfaces
{
    public interface IHideable
    {
        bool Initialized { get; set; }
        
        SpriteRenderer SpriteRenderer { get; set; }

        void DoInitialization();
        
        void Show();

        void Hide();
    }
}