namespace PacMan.Models.Common
{
    public struct Position
    {
        public Position(int left, int top)
        {
            this.Left = left;
            this.Top = top;
        }

        public int Left { get; set; }

        public int Top { get; set; }

        public static bool operator ==(Position first, Position second)
        {
            return (first.Left == second.Left) && (first.Top == second.Top);
        }

        public static bool operator !=(Position first, Position second)
        {
            return (first.Left != second.Left) && (first.Top != second.Top);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Position))
            {
                return false;
            }

            var other = (Position)obj;
            return this == other;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
