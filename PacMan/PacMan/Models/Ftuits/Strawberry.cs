namespace PacMan.Models.Fruits
{
    using System.Windows.Controls;

    using Abstract;
    using Common;

    public class Strawberry : Fruit
    {
        private const int AwardValue = 700;

        public Strawberry(Position position, Image image) : base(position, image, AwardValue)
        {
        }
    }
}
