namespace PacMan.Factories.Builders.EnemyBuilders
{
    using System.Windows.Controls;

    using Logic;
    using Models.Abstract;

    public abstract class EnemyBuilder
    {
        private readonly string[] movementImagesNames = { "up.png", "down.png", "left.png", "rigth.png" };
        private readonly string[] killableImagesNames = { "normal.png", "blinked.png" };

        protected EnemyBuilder(string aliveImagesPath)
        {
            this.AliveImages = this.CreatMovementImagesArray(aliveImagesPath);
            this.CreatInvisibleImagesArray();
            this.CreatKillableImagesArray();
        }

        public Enemy Instance { get; set; }

        protected Image[] AliveImages { get; private set; }

        protected Image[] InvisibleImages { get; private set; }

        protected Image[] KillableImages { get; private set; }
        
        private void CreatKillableImagesArray()
        {
            this.KillableImages = new Image[2];
            for (int index = 0; index < 2; index++)
            {
                this.KillableImages[index] = new Image();
                this.KillableImages[index] = ImageParser.Parse(GlobalConstants.KillableImagesPath +
                    this.killableImagesNames[index]);
            }
        }

        private void CreatInvisibleImagesArray()
        {
            this.InvisibleImages = this.CreatMovementImagesArray(GlobalConstants.InvisibleImagesPath);
        }

        private Image[] CreatMovementImagesArray(string imagePath)
        {
            var images = new Image[4];
            for (int index = 0; index < 4; index++)
            {
                images[index] = new Image();
                images[index] = ImageParser.Parse(imagePath + this.movementImagesNames[index]);
            }

            return images;
        }
    }
}