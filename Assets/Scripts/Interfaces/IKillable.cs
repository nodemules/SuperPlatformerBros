using UnityEngine;

namespace Interfaces
{
    public interface IKillable
    {
        bool Invulnerable { get; set; }
        bool IsDead { get; set; }
        void Kill();
        float DiedAt { get; set; }
        AudioClip DeathAudioClip { get; set; }
    }
}