namespace PacMan.Logic
{
    using System.IO;
    using Models.Common;

    public class HighScore : Score
    {
        private const string HighScoreText = "HIGH SCORE ";
        private const string FilePath = @"/Logic/bestScore.txt";
        private readonly string FileDirectory = Directory.GetParent(@"../../").FullName;
        private readonly int oldScore;

        public HighScore() : base(new Position(0, 15))
        {
            this.Text = HighScoreText;
            this.Value = this.Read();
            this.oldScore = this.Value;
        }

        private int Read()
        {
            var file = new StreamReader(FileDirectory + FilePath);
            var line = file.ReadLine();
            file.Close();
            return int.Parse(line);
        }

        public void Save()
        {
            if (oldScore < this.Value)
            {
                var file = new StreamWriter(FileDirectory + FilePath);
                file.WriteLine(this.Value.ToString());
                file.Close();
            }
        }
    }
}