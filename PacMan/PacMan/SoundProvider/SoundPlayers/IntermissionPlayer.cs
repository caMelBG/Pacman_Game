namespace PacMan.SoundProvider.SoundPlayers
{
    using PacMan.Logic;

    public class IntermissionPlayer : SoundPlayer
    {
        public IntermissionPlayer() : base(GlobalConstants.IntermissionSoundPath, false)
        {
        }
    }
}