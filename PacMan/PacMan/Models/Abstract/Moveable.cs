namespace PacMan.Models.Abstract
{
    using Common;
    using PacMan.Models.Interfaces;

    public abstract class Moveable : GameObject, IGameObject, IMoveable
    {
        private MovementType movementType;
        private Movement movement;

        public Moveable(Position position, Size size, int moveSpeed) : base(position, size)
        {
            this.MoveSpeed = moveSpeed;
        }
        
        public MovementType MovementType
        {
            get
            {
                return this.movementType;
            }

            set
            {
                this.movementType = value;
                this.movement = new Movement(this.MovementType);
            }
        }

        public int MoveSpeed { get; set; }

        public void MakeAMove()
        {
            int left = this.Position.Left + (this.movement.Left * this.MoveSpeed);
            int top = this.Position.Top + (this.movement.Top * this.MoveSpeed);
            this.Position = new Position(left, top);
        }
    }
}