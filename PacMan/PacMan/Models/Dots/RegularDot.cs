namespace PacMan.Models.Dots
{
    using System.Windows.Media;

    using Abstract;
    using Common;

    public class RegularDot : Dot
    {
        public RegularDot(Position position) : base(position, new Size(4, 4), Colors.White)
        {
        }
    }
}
