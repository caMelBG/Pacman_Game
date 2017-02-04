namespace PacMan.Models.Abstract
{
    using System;
    using System.Windows.Controls;

    using Common;
    using Interfaces;
    using Logic;
    using System.Windows.Media;

    public abstract class Fruit : Moveable, IGameObject, IImagable, IMoveable, IAwardable
    {
        private readonly Color color = Colors.Pink;
        private const int FruitMoveSpeed = 2;
        private int fruitBounceCount = 0;
        private Image image;

        public Fruit(Position position, Image image, int awardValue) : base(position, new Size(26, 26), FruitMoveSpeed)
        {
            this.Award = new Award(this.Position, awardValue, color);
            this.image = image;
        }

        public int JumpHeight
        {
            get
            {
                if (this.fruitBounceCount > 5)
                {
                    return 6;
                }

                return 0;
            }
        }

        public bool IsJumped
        {
            get
            {
                return this.fruitBounceCount == 5;
            }
        }
        
        public Award Award { get; set; }

        public Image GetImage()
        {
            this.fruitBounceCount--;
            this.fruitBounceCount = this.fruitBounceCount < 0 ? 10 : this.fruitBounceCount;
            return this.image;
        }
    }
}