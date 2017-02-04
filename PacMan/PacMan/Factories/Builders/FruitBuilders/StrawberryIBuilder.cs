namespace PacMan.Factories.Builders.FruitBuilders
{
    using Logic;
    using Models.Common;
    using Models.Fruits;

    public class StrawberryBuilder : FruitBuilder
    {
        public StrawberryBuilder(Position position) : base(GlobalConstants.StrawberryImagePath)
        {
            this.Instance = new Strawberry(position, this.Image);
        }
    }
}