namespace PacMan.Factories.Builders.FruitBuilders
{
    using Logic;
    using Models.Common;
    using Models.Fruits;

    public class GrapesBuilder : FruitBuilder
    {
        public GrapesBuilder(Position position) : base(GlobalConstants.GrapesImagePath)
        {
            this.Instance = new Grapes(position, this.Image);
        }
    }
}