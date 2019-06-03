namespace PacMan.Factories
{
    using Builders;
    using Builders.EnemyBuilders;
    using Models.Abstract;
    using Models.Common;
    using PacMan.Models.Enums;
    using System;

    public class EnemyFactory
    {
        private EnemyBuilder _enemyBuilder;
    
        public Enemy CreateEnemy(EnemyTypes type, Position position, bool isInCave)
        {
            switch (type)
            {
                case EnemyTypes.Blinky:
                    _enemyBuilder = new BlinkyBuilder(position, isInCave);
                    break;
                case EnemyTypes.Clyde:
                    _enemyBuilder = new ClydeBuilder(position, isInCave);
                    break;
                case EnemyTypes.Inky:
                    _enemyBuilder = new InkyBuilder(position, isInCave);
                    break;
                case EnemyTypes.Pinky:
                    _enemyBuilder = new PinkyBuilder(position, isInCave);
                    break;
                default:
                    throw new ArgumentException($"Not supported player type {type}");
            }

            return _enemyBuilder.Instance;
        }
    }
}
