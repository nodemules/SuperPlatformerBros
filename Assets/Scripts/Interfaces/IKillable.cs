namespace Interfaces
{
    public interface IKillable
    {
        bool Dead { get; set; }
        void Kill();
    }
}