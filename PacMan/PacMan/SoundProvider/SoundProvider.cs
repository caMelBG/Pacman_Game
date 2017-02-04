namespace PacMan.SoundProvider
{
    using PacMan.SoundProvider.SoundPlayers;

    public class SoundProvider
    {
        private readonly ISoundPlayer pacmanMovePlayer;
        private readonly ISoundPlayer pacmanChompPlayer;
        private readonly ISoundPlayer pacmanDeathPlayer;
        private readonly ISoundPlayer fruitStepPlayer;
        private readonly ISoundPlayer eatFruitPlayer;
        private readonly ISoundPlayer eatGhostPlayer;
        private readonly ISoundPlayer scaredPlayer;
        private readonly ISoundPlayer retraitPlayer;
        private readonly ISoundPlayer beginningPlayer;
        private readonly ISoundPlayer intermissionPlayer;
        private readonly ISoundPlayer extraLifePlayer;

        public SoundProvider()
        {
            this.pacmanMovePlayer = new PacmanMovePlayer();
            this.pacmanChompPlayer = new PacmanChompPlayer();
            this.pacmanDeathPlayer = new PacmanDeathPlayer();
            this.fruitStepPlayer = new FruitStepPlayer();
            this.eatFruitPlayer = new EatFruitPlayer();
            this.eatGhostPlayer = new EatGhostPlayer();
            this.scaredPlayer = new ScaredPlayer();
            this.retraitPlayer = new RetraitPlayer();
            this.beginningPlayer = new BeginningPlayer();
            this.intermissionPlayer = new IntermissionPlayer();
            this.extraLifePlayer = new ExtraLifePlayer();
        }

        public void PacmanMovePlay()
        {
            this.pacmanMovePlayer.Play();
        }

        public void PacmanMovePause()
        {
            this.pacmanMovePlayer.Pause();
        }

        public void PacmanDeathPlay()
        {
            this.pacmanDeathPlayer.Play();
        }

        public void ExtraLifePlay()
        {
            this.extraLifePlayer.Play();
        }

        public void BeginningPlay()
        {
            this.beginningPlayer.Play();
        }

        public void IntermissionPlay()
        {
            this.intermissionPlayer.Play();
        }

        public void FruitStepPlay()
        {
            this.fruitStepPlayer.Play();
        }

        public void EatFruitPlay()
        {
            this.eatFruitPlayer.Play();
        }

        public void EatGhostPlay()
        {
            this.eatGhostPlayer.Play();
        }
        
        public void PacmanChompPlay()
        {
            this.pacmanChompPlayer.Play();
        }

        public void PacmanChompPause()
        {
            this.pacmanChompPlayer.Pause();
        }

        public void ScaredPlay()
        {
            this.scaredPlayer.Play();
        }

        public void ScaredPause()
        {
            this.scaredPlayer.Pause();
        }

        public void RetraitPlay()
        {
            this.retraitPlayer.Play();
        }

        public void RetraitPause()
        {
            this.retraitPlayer.Pause();
        }
    }
}