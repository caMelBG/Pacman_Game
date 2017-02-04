namespace PacMan.Models.Fruits
{
    using System.Windows.Controls;

    using Abstract;
    using Common;

    public class Grapes : Fruit
    {
        private const int AwardValue = 500;

        public Grapes(Position position, Image image) : base(position, image, AwardValue)
        {
        }
    }
}
