namespace PacMan.Factories.Builders.FruitBuilders
{
    using System.Windows.Controls;

    using Logic;
    using Models.Abstract;

    public abstract class FruitBuilder 
    {
        protected FruitBuilder(string imagePath)
        {
            this.Image = ImageParser.Parse(imagePath);
        }

        public Fruit Instance { get; set; }

        protected Image Image { get; private set; }
    }
}