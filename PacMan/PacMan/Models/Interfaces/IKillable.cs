namespace PacMan.Models.Interfaces
{
    public interface IKillable
    {
        bool IsAlive { get; set; }

        bool CanKill { get; set; }
    }
}