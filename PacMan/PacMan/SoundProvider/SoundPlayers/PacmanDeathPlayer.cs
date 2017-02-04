namespace PacMan.SoundProvider.SoundPlayers
{
    using PacMan.Logic;

    public class PacmanDeathPlayer : SoundPlayer
    {
        public PacmanDeathPlayer() : base(GlobalConstants.PacmanDeathSoundPath, false)
        {
        }
    }
}