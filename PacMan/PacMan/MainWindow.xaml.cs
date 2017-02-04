namespace PacMan
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using PacMan.Logic;
    using PacMan.Maps;
    using PacMan.Renderers;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            var renderer = new GameRenderer(this.GameCanvas);
            var map = new Classic();
            var engine = new Engine(map, renderer);
            engine.InitGame();
            engine.DrawGameObjects();
            ///this.Dots();
        }

        private void Dots()
        {
            for (int i = 0; i < 650 - 64; i += 18)
            {
                for (int j = 0; j < 650; j += 18)
                {
                    var brush = new SolidColorBrush();
                    brush.Color = Colors.White;
                    var dot = new Ellipse();
                    dot.Width = 2;
                    dot.Height = 2;
                    dot.Fill = brush;

                    Canvas.SetLeft(dot, 9 + i);
                    Canvas.SetTop(dot, 59 + j);
                    this.GameCanvas.Children.Add(dot);
                }
            }
        }
    }
}
