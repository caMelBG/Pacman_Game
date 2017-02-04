namespace PacMan.Factories.Builders.FruitBuilders
{
    using Logic;
    using Models.Common;
    using Models.Fruits;

    public class AppleBuilder : FruitBuilder
    {
        public AppleBuilder(Position position) : base(GlobalConstants.AppleImagePath)
        {
            this.Instance = new Apple(position, this.Image);
        }
    }
}