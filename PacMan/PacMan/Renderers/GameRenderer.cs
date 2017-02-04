namespace PacMan.Renderers
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using Logic;
    using Models.Common;

    public class GameRenderer : IGameRenderer
    {
        private Canvas canvas;

        public GameRenderer(Canvas canvas)
        {
            this.canvas = canvas;
            this.ParentWindow.KeyDown += this.HandleKeyDown;
        }

        public event EventHandler<KeyDownEventArgs> UIActionHappened;

        public Window ParentWindow
        {
            get
            {
                var parent = this.canvas.Parent;
                while (!(parent is Window))
                {
                    parent = LogicalTreeHelper.GetParent(parent);
                }

                return parent as Window;
            }
        }

        public void Clear()
        {
            this.canvas.Children.Clear();
        }
        
        public void Draw(UIElement element, Position position, Models.Common.Size size)
        {
            Canvas.SetLeft(element, position.Left - (size.Width / 2));
            Canvas.SetTop(element, position.Top - (size.Height / 2));
            this.canvas.Children.Add(element);
        }

        private void HandleKeyDown(object sender, KeyEventArgs args)
        {
            var key = args.Key;
            this.UIActionHappened(this, new KeyDownEventArgs(key));
        }
    }
}