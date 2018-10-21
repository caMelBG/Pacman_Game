using System;
using System.Windows.Threading;

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
            DispatcherTimer timer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(60)};
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
