namespace PacMan.Factories.Builders.EnemyBuilders
{
    using Logic;
    using Models.Common;
    using Models.Players;

    public class InkyBuilder : EnemyBuilder
    {
        public InkyBuilder(Position position, bool isInCave) : base(GlobalConstants.InkyImagesPath)
        {
            this.Instance = new Inky(position, isInCave, this.AliveImages, this.InvisibleImages, this.KillableImages);
        }
    }
}