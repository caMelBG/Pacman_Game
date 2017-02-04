namespace PacMan.Models.Interfaces
{
    using System.Windows.Shapes;

    public interface IFigurable : IGameObject
    {
        Shape Figure { get; set; }
    }
}