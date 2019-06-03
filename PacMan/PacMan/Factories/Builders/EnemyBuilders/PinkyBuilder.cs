namespace PacMan.Factories.Builders.EnemyBuilders
{
    using Logic;
    using Models.Common;
    using Models.Players;

    public class PinkyBuilder : EnemyBuilder
    {
        public PinkyBuilder(Position position, bool isInCave) : base(GlobalConstants.PinkyImagesPath)
        {
            this.Instance = new Pinky(position, isInCave, this.AliveImages, this.InvisibleImages, this.KillableImages);
        }
    }
}