namespace Interfaces
{
    public interface IKillable
    {
        bool Invulnerable { get; set; }
        bool Dead { get; set; }
        void Kill();
    }
}