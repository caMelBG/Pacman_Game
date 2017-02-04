namespace PacMan.Models.Common
{
    using System.Windows.Media;
    using System.Windows.Shapes;

    using Abstract;
    using Interfaces;

    public class Gate : GameObject, IGameObject, IFigurable
    {
        public Gate(Position position) : base(position, new Size(45, 5))
        {
            this.Figure = new Rectangle();
            this.Figure.Width = this.Size.Width;
            this.Figure.Height = this.Size.Height;
            var brush = new SolidColorBrush();
            brush.Color = Colors.White;
            this.Figure.Fill = brush;
        }

        public Shape Figure { get; set; }
    }
}