namespace PacMan.Models.Abstract
{
    using System;
    using System.Windows.Media;
    using System.Windows.Shapes;

    using Common;
    using Interfaces;

    public abstract class Dot : GameObject, IGameObject, IFigurable
    {
        public Dot(Position position, Size size, Color color) : base(position, size)
        {
            this.Figure = this.CreateFigure(color);
        }

        public Shape Figure { get; set; }

        public void RecreateFigure()
        {
            var brush = this.Figure.Fill.Clone();
            this.Figure = this.CreateFigure(brush);
        }

        private Shape CreateFigure(Brush brush)
        {
            var figure = new Ellipse();
            figure.Width = this.Size.Width;
            figure.Height = this.Size.Height;
            figure.Fill = brush;
            return figure;
        }

        private Shape CreateFigure(Color color)
        {
            var brush = new SolidColorBrush();
            brush.Color = color;
            return this.CreateFigure(brush);
        }
    }
}