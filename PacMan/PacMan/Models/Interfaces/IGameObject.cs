namespace PacMan.Models.Interfaces
{
    using PacMan.Models.Common;

    public interface IGameObject
    {
        Size Size { get; }

        Position Position { get; set; }
    }
}