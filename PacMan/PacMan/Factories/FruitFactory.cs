namespace PacMan.Factories
{
    using Models.Abstract;
    using Models.Common;
    using PacMan.Factories.Builders.FruitBuilders;

    public class FruitFactory
    {
        private AppleBuilder appleBuilder;
        private CherryBuilder cherryBuilder;
        private GrapesBuilder grapesBuilder;
        private LemonBuilder lemonBuilder;
        private OrangeBuilder orangeBuilder;
        private StrawberryBuilder strawberryBuilder;

        public FruitFactory()
        {
        }

        public Fruit CreatApple(Position position)
        {
            this.appleBuilder = new AppleBuilder(position);
            return this.appleBuilder.Instance;
        }

        public Fruit CreatCherry(Position position)
        {
            this.cherryBuilder = new CherryBuilder(position);
            return this.cherryBuilder.Instance;
        }

        public Fruit CreatGrapes(Position position)
        {
            this.grapesBuilder = new GrapesBuilder(position);
            return this.grapesBuilder.Instance;
        }

        public Fruit CreatOrange(Position position)
        {
            this.orangeBuilder = new OrangeBuilder(position);
            return this.orangeBuilder.Instance;
        }

        public Fruit CreatLemon(Position position)
        {
            this.lemonBuilder = new LemonBuilder(position);
            return this.lemonBuilder.Instance;
        }

        public Fruit CreatStrawberry(Position position)
        {
            this.strawberryBuilder = new StrawberryBuilder(position);
            return this.strawberryBuilder.Instance;
        }
    }
}