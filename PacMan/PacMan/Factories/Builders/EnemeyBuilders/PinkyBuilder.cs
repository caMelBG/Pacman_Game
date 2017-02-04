namespace PacMan.Factories.Builders.EnemeyBuilders
{
    using Logic;
    using Models.Common;
    using Models.Players;

    public class PinkyBuilder : EnemeyBuilder
    {
        public PinkyBuilder(Position position, bool isInCave) : base(GlobalConstants.PinkyImagesPath)
        {
            this.Instance = new Pinky(position, isInCave, this.AliveImages, this.InvisibleImages, this.KillableImages);
        }
    }
}