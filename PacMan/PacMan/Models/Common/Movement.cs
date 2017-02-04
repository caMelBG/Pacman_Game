namespace PacMan.Models.Common
{
    public struct Movement
    {
        public Movement(MovementType movementType)
        {
            this.Left = 0;
            this.Top = 0;
            switch (movementType)
            {
                case MovementType.Up:
                    this.Left = 0;
                    this.Top = -1;
                    break;
                case MovementType.Down:
                    this.Left = 0;
                    this.Top = 1;
                    break;
                case MovementType.Left:
                    this.Left = -1;
                    this.Top = 0;
                    break;
                case MovementType.Right:
                    this.Left = 1;
                    this.Top = 0;
                    break;
            }
        }

        public int Left { get; private set; }

        public int Top { get; private set; }

        public static bool operator ==(Movement first, Movement second)
        {
            return (first.Left == second.Left) && (first.Top == second.Top);
        }

        public static bool operator !=(Movement first, Movement second)
        {
            return (first.Left != second.Left) && (first.Top != second.Top);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Movement))
            {
                return false;
            }

            var other = (Movement)obj;
            return this == other;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}