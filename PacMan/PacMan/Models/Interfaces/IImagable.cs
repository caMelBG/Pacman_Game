namespace PacMan.Models.Interfaces
{
    using System.Windows.Controls;

    public interface IImagable : IGameObject
    {
        Image GetImage();
    }
}
