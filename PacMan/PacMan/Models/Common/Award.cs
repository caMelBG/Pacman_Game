namespace PacMan.Models.Common
{
    using Interfaces;
    using Models.Abstract;

    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class Award : GameObject, IFormatable
    {
        private int isValidCount = 50;
        private Color color;
        private TextBlock textBlock;

        public Award(Position position, int value, Color color) 
            : base(position, new Size(30, 20))
        {
            this.Value = value;
            this.color = color;
        }

        public int Value { get; set; }

        public bool IsValid
        {
            get
            {
                this.isValidCount--;
                if (this.isValidCount > 0)
                {
                    return true;
                }
                else
                {
                    this.isValidCount = 50;
                    return false;
                }
            }
        }

        public TextBlock Format()
        {
            this.textBlock = new TextBlock();
            this.textBlock.Text = this.Value.ToString();
            this.textBlock.Foreground = new SolidColorBrush(this.color);
            this.textBlock.FontWeight = FontWeights.Bold;
            this.textBlock.FontSize = 17;
            return this.textBlock;
        }
    }
}