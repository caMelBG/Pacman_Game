namespace PacMan.Factories.Builders.EnemeyBuilders
{
    using Logic;
    using Models.Common;
    using Models.Players;

    public class BlinkyBuilder : EnemeyBuilder
    {
        public BlinkyBuilder(Position position, bool isInCave) : base(GlobalConstants.BlinkyImagesPath)
        {
            this.Instance = new Blinky(position, isInCave, this.AliveImages, this.InvisibleImages, this.KillableImages);
        }
    }
}
