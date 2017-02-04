namespace PacMan.SoundProvider.SoundPlayers
{
    using PacMan.Logic;

    public class FruitStepPlayer : SoundPlayer
    {
        public FruitStepPlayer() : base(GlobalConstants.FruitStepSoundPath, false)
        {
        }
    }
}