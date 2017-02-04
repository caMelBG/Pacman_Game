namespace PacMan.SoundProvider.SoundPlayers
{
    using PacMan.Logic;

    public class ExtraLifePlayer : SoundPlayer
    { 
        public ExtraLifePlayer() : base(GlobalConstants.ExtraLifeSoundPath, false)
        {
        }
    }
}