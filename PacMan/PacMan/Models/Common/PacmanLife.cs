namespace PacMan.Models.Common
{
    using System.Windows.Controls;

    using Abstract;
    using PacMan.Logic;
    using PacMan.Models.Interfaces;

    public class PacmanLife : GameObject, IImagable
    {
        private const int DistanceFromTop = 636;
        private const int PacmanStartLives = 4;
        private const string LifeImageFileName = "leftFirst.png";

        public PacmanLife() 
            : base(new Position(0, DistanceFromTop), new Size(30, 30))
        {
            this.Count = PacmanStartLives;
        }

        public int Count { get; set; }

        public Image GetImage()
        {
            return ImageParser.Parse(GlobalConstants.PacmanAliveImagesPath + LifeImageFileName);
        }
    }
}