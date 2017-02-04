namespace PacMan.Factories.Builders
{
    using System.Windows.Controls;
    
    using Logic;
    using Models.Abstract;
    using Models.Common;
    using Models.Players;

    public class PacmanBuilder
    {
        private const string ImagesFileExtension = ".png";
        private readonly string basicImageName = "basic.png";
        private readonly string[] movementImagesNames = { "upFirst.png", "upSecond.png", "downFirst.png", "downSecond.png", "leftFirst.png", "leftSecond.png", "rigthFirst.png", "rigthSecond.png" };
        private Image[][] aliveImages;
        private Image[] dyingImages;

        public PacmanBuilder(Position position)
        {
            this.CreatAlivePacmanImagesArray();
            this.CreatDyingPacmanImagesArray();
            this.Instance = new Pacman(position, this.aliveImages, this.dyingImages);
        }

        public Pacman Instance { get; private set; }

        private void CreatAlivePacmanImagesArray()
        {
            this.aliveImages = new Image[4][];
            for (int index = 0; index < 4; index++)
            {
                this.aliveImages[index] = new Image[3];
                this.aliveImages[index][0] = ImageParser.Parse(GlobalConstants.PacmanAliveImagesPath + this.basicImageName);
            }

            for (int index = 0; index < this.movementImagesNames.Length; index++)
            {
                int x = index / 2;
                int y = (index % 2) + 1;
                this.aliveImages[x][y] = ImageParser.Parse(GlobalConstants.PacmanAliveImagesPath + this.movementImagesNames[index]);
            }
        }

        private void CreatDyingPacmanImagesArray()
        {
            this.dyingImages = new Image[10];
            for (int index = 0; index < 10; index++)
            {
                this.dyingImages[index] = new Image();
                this.dyingImages[index] = ImageParser.Parse(GlobalConstants.PacmanDyingImagePath + index.ToString() + ImagesFileExtension);
            }
        }
    }
}