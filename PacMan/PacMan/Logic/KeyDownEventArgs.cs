namespace PacMan.Logic
{
    using Models.Common;
    using System;
    using System.Windows.Input;

    public class KeyDownEventArgs : EventArgs
    {
        public KeyDownEventArgs(Key key)
        {
            switch (key)
            {
                case Key.Up:
                    this.MovementType = MovementType.Up;
                    break;
                case Key.Down:
                    this.MovementType = MovementType.Down;
                    break;
                case Key.Left:
                    this.MovementType = MovementType.Left;
                    break;
                case Key.Right:
                    this.MovementType = MovementType.Right;
                    break;
            }
        }

        public MovementType MovementType { get; set; }
    }
}
