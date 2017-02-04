namespace PacMan.Models.Players
{
    using System.Windows.Controls;

    using Abstract;
    using Common;
    using Interfaces;

    public class Pacman : Player, IGameObject, IMoveable, IImagable
    {
        private int tempIndex;
        private int aliveSpriteIndex;
        private int dyingSpriteIndex;
        private Image[][] aliveSprites;
        private Image[] dyingSprites;

        public Pacman(Position position, Image[][] aliveSprites, Image[] dyingSprites)
            : base(position, true)
        {
            this.tempIndex = 0;
            this.aliveSpriteIndex = 0;
            this.dyingSpriteIndex = 0;
            this.aliveSprites = aliveSprites;
            this.dyingSprites = dyingSprites;
            this.MoveSpeed = 3;
        }

        public Image GetImage()
        {
            int index = 0;
            if (!this.IsAlive)
            {
                index = this.dyingSpriteIndex / 4;
                if (index < 10)
                {
                    this.dyingSpriteIndex++;
                    return this.dyingSprites[index];
                }
                else
                {
                    return this.dyingSprites[index - 1];
                }
            }

            if (this.aliveSpriteIndex % 4 == 0)
            {
                this.tempIndex = this.aliveSpriteIndex / 4;
            }

            this.aliveSpriteIndex++;
            this.aliveSpriteIndex = this.aliveSpriteIndex >= 12 ? 0 : this.aliveSpriteIndex;
            return this.aliveSprites[(int)this.MovementType][this.tempIndex];
        }
    }
}