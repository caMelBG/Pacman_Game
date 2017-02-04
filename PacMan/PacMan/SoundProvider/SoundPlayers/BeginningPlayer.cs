namespace PacMan.SoundProvider.SoundPlayers
{
    using PacMan.Logic;

    public class BeginningPlayer : SoundPlayer
    {
        public BeginningPlayer() : base(GlobalConstants.BeginningSoundPath, false)
        {
        }
    }
}