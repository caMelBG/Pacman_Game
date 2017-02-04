namespace PacMan.Models.Dots
{
    using System.Windows.Media;

    using Abstract;
    using Common;

    public class HeavyDot : Dot
    {
        public HeavyDot(Position position) : base(position, new Size(12, 12), Colors.White)
        {
        }
    }
}