namespace PacMan.Models.Abstract
{
    using Common;
    using Interfaces;

    public abstract class Player : Moveable, IGameObject, IMoveable
    {
        private const int PlayerDefaultSpeed = 3;

        public Player(Position position, bool isAlive)
            : base(position, new Size(30, 30), PlayerDefaultSpeed)
        {
            this.IsAlive = isAlive;
        }

        public bool IsAlive { get; set; }
    }
}