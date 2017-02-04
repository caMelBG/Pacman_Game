namespace PacMan.Factories.Builders.FruitBuilders
{
    using Logic;
    using Models.Common;
    using Models.Fruits;

    public class CherryBuilder : FruitBuilder
    {
        public CherryBuilder(Position position) : base(GlobalConstants.CherryImagePath)
        {
            this.Instance = new Cherry(position, this.Image);
        }
    }
}
