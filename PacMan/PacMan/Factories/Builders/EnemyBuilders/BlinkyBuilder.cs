namespace PacMan.Factories.Builders.EnemyBuilders
{
    using Logic;
    using Models.Common;
    using Models.Players;

    public class BlinkyBuilder : EnemyBuilder
    {
        public BlinkyBuilder(Position position, bool isInCave) : base(GlobalConstants.BlinkyImagesPath)
        {
            this.Instance = new Blinky(position, isInCave, this.AliveImages, this.InvisibleImages, this.KillableImages);
        }
    }
}
