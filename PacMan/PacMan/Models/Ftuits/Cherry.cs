namespace PacMan.Models.Fruits
{
    using System.Windows.Controls;

    using Abstract;
    using Common;

    public class Cherry : Fruit
    {
        private const int AwardValue = 300;

        public Cherry(Position position, Image image) : base(position, image, AwardValue)
        {
        }
    }
}
