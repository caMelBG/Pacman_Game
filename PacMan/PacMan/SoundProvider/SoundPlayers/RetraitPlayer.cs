namespace PacMan.SoundProvider.SoundPlayers
{
    using PacMan.Logic;

    public class RetraitPlayer : SoundPlayer
    {
        public RetraitPlayer() : base(GlobalConstants.RetraitSoundPath, true)
        {
        }
    }
}