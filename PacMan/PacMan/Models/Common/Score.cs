namespace PacMan.Logic
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Models.Abstract;
    using Models.Common;
    using Maps;
    using Models.Interfaces;
    using PacMan.Common;

    public class Score : GameObject, IFormatable
    {
        private const string HighScoreText = "SCORE ";

        public Score() : this(new Position(ClassicMapConstants.MapWidth / 2 + 25, 15))
        {

            this.Text = HighScoreText;
            this.Value = 0;
        }
        
        protected Score(Position position) : base(position, new Models.Common.Size(0, 20))
        {
        }

        public int Value { get; set; }

        protected string Text { get; set; }

        public TextBlock Format()
        {
            var textBlock = new TextBlock();
            textBlock.Text = this.Text + this.Value.ToString();
            textBlock.Foreground = new SolidColorBrush(Colors.White);
            textBlock.FontSize = 20;
            textBlock.FontWeight = FontWeights.Bold;
            return textBlock;
        }
    }
}