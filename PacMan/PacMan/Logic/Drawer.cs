namespace PacMan.Logic
{
    using Maps;
    using Models.Common;
    using PacMan.Common;
    using PacMan.Models.Dots;
    using PacMan.Models.Enums;

    public partial class Engine 
    {
        public void DrawGameObjects()
        {
            this.gameRenderer.Clear();
            this.gameRenderer.Draw(this.currentScore.Format(), currentScore.Position, currentScore.Size);
            this.gameRenderer.Draw(this.bestScore.Format(), this.bestScore.Position, this.bestScore.Size);
            this.gameRenderer.Draw(this.gameMap.GetImage(), this.gameMap.Position, this.gameMap.Size);
            this.gameRenderer.Draw(this.gate.Figure, this.gate.Position, this.gate.Size);   
            
            ///DRAW REGULAR DOTS
            foreach (var position in this.regularDotPositions)
            {
                var regularDot = dotFactory.CreateDot(DotTypes.Regular, position);
                this.gameRenderer.Draw(regularDot.Figure, regularDot.Position, regularDot.Size);
            }

            ///DRAW HEAVY DOTS
            foreach (var position in this.heavyDotPositions)
            {
                var heavyDot = dotFactory.CreateDot(DotTypes.Heavy, position);
                this.gameRenderer.Draw(heavyDot.Figure, heavyDot.Position, heavyDot.Size);
            }

            ///DRAW ENEMYS
            foreach (var enemy in this.enemeis)
            {
                this.gameRenderer.Draw(enemy.GetImage(), enemy.Position, enemy.Size);
            }

            ///DRAW FRUIT
            if (this.fruit != null)
            {
                var height = this.fruit.JumpHeight;
                this.fruit.Position = new Position(this.fruit.Position.Left, this.fruit.Position.Top + height);
                this.gameRenderer.Draw(this.fruit.GetImage(), this.fruit.Position, this.fruit.Size);
                this.fruit.Position = new Position(this.fruit.Position.Left, this.fruit.Position.Top - height);
            }

            ///DRAW EATEN FRUITS
            if (this.eatenFruits.Count != 0)
            {
                for (int index = 0; index < this.eatenFruits.Count; index++)
                {
                    var currFruit = this.eatenFruits[index];
                    currFruit.Position = new Position(ClassicMapConstants.MapWidth - (ClassicMapConstants.DistanceBetweenGameObjects + (index * DistanceBetweenEachEatenFruit)), DistanceOfEatenFruitsFromTop);
                    this.gameRenderer.Draw(currFruit.GetImage(), currFruit.Position, currFruit.Size);
                }
            }

            ///DRAW LIFES ICONS
            for (int index = 0; index < this.pacmanLife.Count; index++)
            {
                this.pacmanLife.Position = new Position(ClassicMapConstants.DistanceBetweenGameObjects + (index * DistanceBetweenEachEatenFruit), this.pacmanLife.Position.Top);
                this.gameRenderer.Draw(this.pacmanLife.GetImage(), this.pacmanLife.Position, this.pacmanLife.Size);
            }

            ///DRAW AWARDS
            if (this.earnedAward != null)
            {
                if (earnedAward.IsValid)
                {
                    this.gameRenderer.Draw(this.earnedAward.Format(), this.earnedAward.Position, this.earnedAward.Size);
                }
                else
                {
                    this.earnedAward = null;
                }
            }

            this.gameRenderer.Draw(this.pacman.GetImage(), this.pacman.Position, this.pacman.Size);
        }
    }
}