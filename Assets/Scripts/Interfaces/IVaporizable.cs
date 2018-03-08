using UnityEngine;

namespace Interfaces
{
    public interface IVaporizable : IKillable
    {
        bool Vaporizable { get; set; }
        AudioClip VaporizeAudioClip { get; set; }
        void Vaporize();
    }
}