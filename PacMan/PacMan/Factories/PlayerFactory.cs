namespace PacMan.Factories
{
    using Builders;
    using Builders.EnemeyBuilders;
    using Models.Abstract;
    using Models.Common;
    using Models.Players;
    public class PlayerFactory
    {
        private PacmanBuilder pacmanBuilder;
        private BlinkyBuilder blinkyBuilder;
        private InkyBuilder inkyBuilder;
        private PinkyBuilder pinkyBuilder;
        private ClydeBuilder clydeBuilder;

        public PlayerFactory()
        {
        }

        public Pacman CreatPacman(Position position)
        {
            this.pacmanBuilder = new PacmanBuilder(position);
            return this.pacmanBuilder.Instance;
        }

        public Enemey CreatBlinky(Position position, bool isInCave)
        {
            this.blinkyBuilder = new BlinkyBuilder(position, isInCave);
            return this.blinkyBuilder.Instance;
        }

        public Enemey CreatInky(Position position, bool isInCave)
        {
            this.inkyBuilder = new InkyBuilder(position, isInCave);
            return this.inkyBuilder.Instance;
        }

        public Enemey CreatPinky(Position position, bool isInCave)
        {
            this.pinkyBuilder = new PinkyBuilder(position, isInCave);
            return this.pinkyBuilder.Instance;
        }

        public Enemey CreatClyde(Position position, bool isInCave)
        {
            this.clydeBuilder = new ClydeBuilder(position, isInCave);
            return this.clydeBuilder.Instance;
        }
    }
}