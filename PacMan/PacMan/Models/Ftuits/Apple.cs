namespace PacMan.Models.Fruits
{
    using System.Windows.Controls;

    using Abstract;
    using Common;

    public class Apple : Fruit
    {
        private const int AwardValue = 300;

        public Apple(Position position, Image image) : base(position, image, AwardValue)
        {
        }
    }
}