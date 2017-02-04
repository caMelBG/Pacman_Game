namespace PacMan.Factories.Builders.FruitBuilders
{
    using Logic;
    using Models.Common;
    using Models.Fruits;

    public class LemonBuilder : FruitBuilder
    {
        public LemonBuilder(Position position) : base(GlobalConstants.LemonImagePath)
        {
            this.Instance = new Lemon(position, this.Image);
        }
    }
}