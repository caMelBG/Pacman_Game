namespace PacMan.SoundProvider.SoundPlayers
{
    using PacMan.Logic;

    public class EatGhostPlayer : SoundPlayer
    {
        public EatGhostPlayer() : base(GlobalConstants.EatGhostSoundPath, false)
        {
        }
    }
}
