namespace PacMan.Models.Fruits
{
    using System.Windows.Controls;

    using Abstract;
    using Common;

    public class Orange : Fruit
    {
        private const int AwardValue = 700;

        public Orange(Position position, Image image) : base(position, image, AwardValue)
        {
        }
    }
}
