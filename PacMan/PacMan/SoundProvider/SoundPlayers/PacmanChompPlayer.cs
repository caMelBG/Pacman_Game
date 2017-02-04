namespace PacMan.SoundProvider.SoundPlayers
{
    using PacMan.Logic;

    public class PacmanChompPlayer : SoundPlayer
    {
        public PacmanChompPlayer() : base(GlobalConstants.PacmanChompSoundPath, true)
        {
        }
    }
}