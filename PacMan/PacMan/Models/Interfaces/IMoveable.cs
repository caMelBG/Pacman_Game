namespace PacMan.Models.Interfaces
{
    using PacMan.Models.Common;

    public interface IMoveable : IGameObject
    {
        MovementType MovementType { get; set; }
        
        void MakeAMove();
    }
}