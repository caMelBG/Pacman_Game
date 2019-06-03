namespace PacMan.Models.Abstract
{
    using System.Windows.Controls;
    using PacMan.Models.Common;
    using Interfaces;
    using Logic;
    using System;
    using System.Windows.Media;

    public abstract class Enemy : Player, IGameObject, IMoveable, IKillable, IImagable, IAwardable
    {
        private const int EnemyAwardValue = 200;
        private readonly Color color = Colors.SkyBlue;
        private Image[] aliveSprites;
        private Image[] invisibleSprites;
        private Image[] killableSprites;

        private bool canKill;
        private int killableSpriteIndex = 0;
        private int killableSpriteTimer = 500;

        public Enemy(Position position, bool canKill, bool isAlive, bool isInCave, Image[] aliveSprites, Image[] invisibleSprites, Image[] killableSprites)
            : base(position, isAlive)
        {
            this.Award = new Award(this.Position, EnemyAwardValue, color);
            this.aliveSprites = aliveSprites;
            this.invisibleSprites = invisibleSprites;
            this.killableSprites = killableSprites;
            this.IsInCave = isInCave;
            this.CanKill = canKill;
        }

        public bool CanKill
        {
            get
            {
                return this.canKill;
            }

            set
            {
                this.killableSpriteTimer = 500;
                this.canKill = value;
            }
        }

        public bool IsInCave { get; set; }

        public Award Award { get; set; }

        public Image GetImage()
        {
            if (this.IsAlive)
            {
                if (this.CanKill)
                {
                    if (this.killableSpriteTimer < 200 && (this.killableSpriteTimer % 10) < 5)
                    {
                        this.killableSpriteIndex = 1;
                    }
                    else
                    {
                        this.killableSpriteIndex = 0;
                    }

                    this.killableSpriteTimer--;
                    if (this.killableSpriteTimer == 0)
                    {
                        this.killableSpriteTimer = 500;
                        this.CanKill = false;
                    }

                    return this.killableSprites[this.killableSpriteIndex];
                }

                return this.aliveSprites[(int)this.MovementType];
            }
            else if (this.CanKill)
            {
                this.killableSpriteTimer = 500;
                this.CanKill = false;
            }

            return this.invisibleSprites[(int)this.MovementType];
        }
    }
}