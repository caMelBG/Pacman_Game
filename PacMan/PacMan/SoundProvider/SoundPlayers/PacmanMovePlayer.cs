namespace PacMan.SoundProvider.SoundPlayers
{
    using PacMan.Logic;

    public class PacmanMovePlayer : SoundPlayer
    {
        public PacmanMovePlayer() : base(GlobalConstants.PacmanMoveSoundPath, true, 1.0f)
        {
        }
    }
}
