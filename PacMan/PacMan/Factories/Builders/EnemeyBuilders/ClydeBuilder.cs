namespace PacMan.Factories.Builders.EnemeyBuilders
{
    using Logic;
    using Models.Common;
    using Models.Players;

    public class ClydeBuilder : EnemeyBuilder
    {
        public ClydeBuilder(Position position, bool isInCave) : base(GlobalConstants.ClydeImagesPath)
        {
            this.Instance = new Clyde(position, isInCave, this.AliveImages, this.InvisibleImages, this.KillableImages);
        }
    }
}