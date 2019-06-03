namespace PacMan.Models.Players
{
    using System.Windows.Controls;

    using Abstract;
    using Common;

    public class Inky : Enemy
    {
        public Inky(Position position, bool isInCave, Image[] aliveSprites, Image[] invisibleSprites, Image[] killableSprites) 
            : base(position, false, true, isInCave, aliveSprites, invisibleSprites, killableSprites)
        {
        }
    }
}