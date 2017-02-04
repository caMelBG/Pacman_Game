namespace PacMan.Models.Abstract
{
    using Common;
    using PacMan.Models.Interfaces;

    public abstract class GameObject : IGameObject
    {
        public GameObject(Position position, Size size)
        {
            this.Position = position;
            this.Size = size;
        }

        public virtual Position Position { get; set; }

        public Size Size { get; }

        public override bool Equals(object obj)
        {
            return this.Position == (obj as GameObject).Position;
        }

        public override int GetHashCode()
        {
            return this.Position.GetHashCode();
        }
    }
}