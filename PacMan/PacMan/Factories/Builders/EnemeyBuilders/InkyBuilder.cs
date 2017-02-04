namespace PacMan.Factories.Builders.EnemeyBuilders
{
    using Logic;
    using Models.Common;
    using Models.Players;

    public class InkyBuilder : EnemeyBuilder
    {
        public InkyBuilder(Position position, bool isInCave) : base(GlobalConstants.InkyImagesPath)
        {
            this.Instance = new Inky(position, isInCave, this.AliveImages, this.InvisibleImages, this.KillableImages);
        }
    }
}