namespace PacMan.Models.Abstract
{
    using System.Windows.Media;
    using System.Windows.Shapes;

    using Common;
    using Interfaces;

    public abstract class Dot : GameObject, IGameObject, IFigurable
    {
        public Dot(Position position, Size size, Color color) : base(position, size)
        {
            this.Figure = new Ellipse();
            this.Figure.Width = this.Size.Width;
            this.Figure.Height = this.Size.Height;
            var brush = new SolidColorBrush();
            brush.Color = color;
            this.Figure.Fill = brush;
        }

        public Shape Figure { get; set; }
    }
}