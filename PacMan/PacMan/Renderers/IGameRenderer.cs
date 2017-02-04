namespace PacMan.Renderers
{
    using System;
    using System.Windows;

    using Logic;
    using Models.Common;

    public interface IGameRenderer
    {
        event EventHandler<KeyDownEventArgs> UIActionHappened;
        
        void Draw(UIElement element, Position position, Models.Common.Size size);

        void Clear();
    }
}