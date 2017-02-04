namespace PacMan.SoundProvider.SoundPlayers
{
    using PacMan.Logic;

    public class ScaredPlayer : SoundPlayer
    {
        public ScaredPlayer() : base(GlobalConstants.ScaredSoundPath, true, 0.5f)
        {
        }
    }
}