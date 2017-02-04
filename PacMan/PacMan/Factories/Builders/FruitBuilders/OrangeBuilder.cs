namespace PacMan.Factories.Builders.FruitBuilders
{
    using Logic;
    using Models.Common;
    using Models.Fruits;

    public class OrangeBuilder : FruitBuilder
    {
        public OrangeBuilder(Position position) : base(GlobalConstants.OrangeImagePath)
        {
            this.Instance = new Orange(position, this.Image);
        }
    }
}