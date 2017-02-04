namespace PacMan.Models.Fruits
{
    using System.Windows.Controls;

    using Abstract;
    using Common;

    public class Lemon : Fruit
    {
        private const int AwardValue = 500;

        public Lemon(Position position, Image image) : base(position, image, AwardValue)
        {
        }
    }
}
