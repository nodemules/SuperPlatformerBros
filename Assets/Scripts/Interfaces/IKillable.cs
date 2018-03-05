namespace Interfaces
{
    public interface IKillable
    {
        bool Invulnerable { get; set; }
        bool IsDead { get; set; }
        void Kill();
    }
}