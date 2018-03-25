namespace PlayerCharacter
{
    public interface IPlayer
    {
        int Health { get; set; }
        int MaxHealth { get; set; }
        int Experience { get; set; }
    }
}