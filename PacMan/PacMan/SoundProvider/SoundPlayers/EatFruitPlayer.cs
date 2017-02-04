namespace PacMan.SoundProvider.SoundPlayers
{
    using PacMan.Logic;

    public class EatFruitPlayer : SoundPlayer
    {
        public EatFruitPlayer() : base(GlobalConstants.EatFruitSoundPath, false)
        {
        }
    }
}
