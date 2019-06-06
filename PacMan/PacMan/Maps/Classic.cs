namespace PacMan.Maps
{
    using System.Collections.Generic;
    using System.Windows.Controls;

    using Logic;
    using Factories;
    using Models.Abstract;
    using Models.Common;
    using Models.Interfaces;
    using Models.Players;
    using PacMan.Models.Enums;
    using PacMan.Factories.Builders;
    using PacMan.Common;
    using PacMan.Maps.Strategies;

    public class Classic : GameObject, IMap, IImagable
    {
        private readonly Position fruitStartPosition =
            new Position((ClassicMapConstants.DistanceBetweenGameObjects * 13) + (ClassicMapConstants.DistanceBetweenGameObjects / 2) + 1 + ClassicMapConstants.DistanceFromTheLeft, (ClassicMapConstants.DistanceBetweenGameObjects * 17) + ClassicMapConstants.DistanceFromTheTop);
        private readonly Position pacmanStartPosition =
            new Position((ClassicMapConstants.DistanceBetweenGameObjects * 13) + (ClassicMapConstants.DistanceBetweenGameObjects / 2) + ClassicMapConstants.DistanceFromTheLeft, (ClassicMapConstants.DistanceBetweenGameObjects * 23) + ClassicMapConstants.DistanceFromTheTop);
        private readonly Position pinkyStartPosition =
            new Position((ClassicMapConstants.DistanceBetweenGameObjects * 11) + (ClassicMapConstants.DistanceBetweenGameObjects / 2) + ClassicMapConstants.DistanceFromTheLeft, (ClassicMapConstants.DistanceBetweenGameObjects * 14) + ClassicMapConstants.DistanceFromTheTop);
        private readonly Position inkyStartPosition =
            new Position((ClassicMapConstants.DistanceBetweenGameObjects * 13) + (ClassicMapConstants.DistanceBetweenGameObjects / 2) + ClassicMapConstants.DistanceFromTheLeft, (ClassicMapConstants.DistanceBetweenGameObjects * 14) + ClassicMapConstants.DistanceFromTheTop);
        private readonly Position blinkyStartPosition =
            new Position((ClassicMapConstants.DistanceBetweenGameObjects * 13) + (ClassicMapConstants.DistanceBetweenGameObjects / 2) + ClassicMapConstants.DistanceFromTheLeft, (ClassicMapConstants.DistanceBetweenGameObjects * 11) + ClassicMapConstants.DistanceFromTheTop);
        private readonly Position clydeStartPosition =
            new Position((ClassicMapConstants.DistanceBetweenGameObjects * 15) + (ClassicMapConstants.DistanceBetweenGameObjects / 2) + ClassicMapConstants.DistanceFromTheLeft, (ClassicMapConstants.DistanceBetweenGameObjects * 14) + ClassicMapConstants.DistanceFromTheTop);

        private int[,] board;
        private Image image;
        private PacmanBuilder pacmanBuilder;
        private EnemyFactory playerFactory;
        private FruitFactory fruitFactory;

        private IPathfinderStrategy pathToCage = new FindPathToCageStrategy();
        private IPathfinderStrategy pathOutOfCage = new FindPathOutOfCageStrategy();


        public Classic() :
            base(new Position(ClassicMapConstants.MapWidth / 2, (ClassicMapConstants.MapHeight / 2) + (ClassicMapConstants.DistanceFromTheTop - ClassicMapConstants.DistanceFromTheLeft)), new Size(ClassicMapConstants.MapWidth, ClassicMapConstants.MapHeight))
        {
            this.InitBoard();
            this.playerFactory = new EnemyFactory();
            this.fruitFactory = new FruitFactory();
            this.image = ImageParser.Parse(GlobalConstants.ClassicMapImagePath);
        }

        public bool CanMove(IMoveable gameObject, MovementType movementType)
        {
            var movement = new Movement(movementType);
            int row = gameObject.Position.Top - ClassicMapConstants.DistanceFromTheTop;
            int col = gameObject.Position.Left - ClassicMapConstants.DistanceFromTheLeft;
            if (row % ClassicMapConstants.DistanceBetweenGameObjects == 0 && col % ClassicMapConstants.DistanceBetweenGameObjects == 0)
            {
                row /= ClassicMapConstants.DistanceBetweenGameObjects;
                col /= ClassicMapConstants.DistanceBetweenGameObjects;
                col += movement.Left;
                row += movement.Top;

                if (col >= ClassicMapConstants.BoardCols)
                {
                    col = col - ClassicMapConstants.BoardCols;
                    col = ClassicMapConstants.BoardCols - col - 1;
                }

                ///TELEPORT
                if (col < 0)
                {
                    if (gameObject is Models.Players.Pacman)
                    {
                        if (gameObject.Position.Left == ClassicMapConstants.DistanceFromTheLeft)
                        {
                            gameObject.Position = new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 28), gameObject.Position.Top);
                        }
                        else
                        {
                            gameObject.Position = new Position(ClassicMapConstants.DistanceFromTheLeft, gameObject.Position.Top);
                        }

                        return true;
                    }
                    else
                    {
                        if (gameObject.Position.Left == ClassicMapConstants.DistanceFromTheLeft)
                        {
                            gameObject.MovementType = MovementType.Right;
                        }
                        else
                        {
                            gameObject.MovementType = MovementType.Left;
                        }

                        return false;
                    }
                }

                return this.board[row, col] > 1;
            }
            else if (row % ClassicMapConstants.DistanceBetweenGameObjects == 0)
            {
                if (movementType == MovementType.Left || movementType == MovementType.Right)
                {
                    return true;
                }
            }
            else if (col % ClassicMapConstants.DistanceBetweenGameObjects == 0)
            {
                if (movementType == MovementType.Up || movementType == MovementType.Down)
                {
                    return true;
                }
            }

            return false;
        }

        public Image GetImage()
        {
            return this.image;
        }

        public Gate InitGate()
        {
            return new Gate(new Position((ClassicMapConstants.DistanceBetweenGameObjects * 13) + (ClassicMapConstants.DistanceBetweenGameObjects / 2) + ClassicMapConstants.DistanceFromTheLeft, (ClassicMapConstants.DistanceBetweenGameObjects * 12) + (ClassicMapConstants.DistanceBetweenGameObjects / 6) + ClassicMapConstants.DistanceFromTheTop));
        }

        public Fruit InitFruit()
        {
            switch (RandomNumberGenerator.Next(6))
            {
                case 0: return this.fruitFactory.CreateFruit(FruitTypes.Apple, this.fruitStartPosition);
                case 1: return this.fruitFactory.CreateFruit(FruitTypes.Cherry, this.fruitStartPosition);
                case 2: return this.fruitFactory.CreateFruit(FruitTypes.Grapes, this.fruitStartPosition);
                case 3: return this.fruitFactory.CreateFruit(FruitTypes.Lemon, this.fruitStartPosition);
                case 4: return this.fruitFactory.CreateFruit(FruitTypes.Orange, this.fruitStartPosition);
                case 5: return this.fruitFactory.CreateFruit(FruitTypes.Strawberry, this.fruitStartPosition);
            }

            return this.fruitFactory.CreateFruit(FruitTypes.Apple, this.fruitStartPosition);
        }

        public Pacman InitPacMan()
        {
            this.pacmanBuilder = new PacmanBuilder(this.pacmanStartPosition);
            return this.pacmanBuilder.Instance;
        }

        public IEnumerable<Enemy> InitEnemies()
        {
            var enemies = new List<Enemy>();
            enemies.Add(this.playerFactory.CreateEnemy(EnemyTypes.Blinky, this.pinkyStartPosition, true));
            enemies.Add(this.playerFactory.CreateEnemy(EnemyTypes.Clyde, this.inkyStartPosition, true));
            enemies.Add(this.playerFactory.CreateEnemy(EnemyTypes.Inky, this.clydeStartPosition, true));
            enemies.Add(this.playerFactory.CreateEnemy(EnemyTypes.Pinky, this.blinkyStartPosition, false));
            return enemies;
        }

        public IEnumerable<Position> InitRegularCoins()
        {
            var regularCoins = new List<Position>();
            ///12 points
            for (int index = 0; index < (12 * ClassicMapConstants.DistanceBetweenGameObjects); index += ClassicMapConstants.DistanceBetweenGameObjects)
            {
                regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 2) + index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 5)));
                regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 2) - index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 5)));
                regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 1) + index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 1)));
                regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 1) - index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 1)));
                regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 1) + index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 20)));
                regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 1) - index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 20)));
                regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 2) + index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 29)));
                regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 2) - index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 29)));

                ///11 points
                if (index < (11 * ClassicMapConstants.DistanceBetweenGameObjects))
                {
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 6), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 9) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 6), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 9) + index));
                }

                ///6 points
                if (index < (6 * ClassicMapConstants.DistanceBetweenGameObjects))
                {
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 6), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 21) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 6), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 21) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 7) + index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 23)));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 7) - index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 23)));
                }

                ///5 points
                if (index < (5 * ClassicMapConstants.DistanceBetweenGameObjects))
                {
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 1) + index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 26)));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 1) - index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 26)));
                }

                ///4 points
                if (index < (4 * ClassicMapConstants.DistanceBetweenGameObjects))
                {
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 2) + index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 8)));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 2) - index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 8)));
                }

                ///3 points
                if (index < (3 * ClassicMapConstants.DistanceBetweenGameObjects))
                {
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 1), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 6) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 1), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 6) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 6), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 2) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 6), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 2) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 12), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 2) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 12), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 2) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 6), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 6) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 6), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 6) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 9), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 6) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 9), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 6) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 10) + index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 8)));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 10) - index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 8)));
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 1), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 27) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 1), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 27) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 9), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 24) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 9), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 24) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 12), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 26) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 12), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 26) + index));
                }

                ///2 points
                if (index < (2 * ClassicMapConstants.DistanceBetweenGameObjects))
                {
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 1), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 4) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 1), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 4) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 1), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 21) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 1), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 21) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 3), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 24) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 3), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 24) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 12), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 21) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 12), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 21) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 2) + index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 23)));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 2) - index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 23)));
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 10) + index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 26)));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 10) - index, ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 26)));
                }

                ///1 point
                if (index < ClassicMapConstants.DistanceBetweenGameObjects)
                {
                    regularCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 1), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 2) + index));
                    regularCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 1), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 2) + index));
                }
            }

            return regularCoins;
        }

        public IEnumerable<Position> InitHeavyCoins()
        {
            var heavyCoins = new List<Position>();
            heavyCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 1), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 3)));
            heavyCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 1), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 3)));
            heavyCoins.Add(new Position(ClassicMapConstants.DistanceFromTheLeft + (ClassicMapConstants.DistanceBetweenGameObjects * 1), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 23)));
            heavyCoins.Add(new Position(ClassicMapConstants.MapWidth - ClassicMapConstants.DistanceFromTheLeft - (ClassicMapConstants.DistanceBetweenGameObjects * 1), ClassicMapConstants.DistanceFromTheTop + (ClassicMapConstants.DistanceBetweenGameObjects * 23)));
            return heavyCoins;
        }

        public MovementType FindWayOutOfCave(IMoveable gameObject)
        {
            return pathOutOfCage.FindPath(gameObject, board);
            if ((gameObject.Position.Top - ClassicMapConstants.DistanceFromTheTop) % ClassicMapConstants.DistanceBetweenGameObjects == 0 &&
                (gameObject.Position.Left - ClassicMapConstants.DistanceFromTheLeft) % ClassicMapConstants.DistanceBetweenGameObjects == 0)
            {
                int row = (gameObject.Position.Top - ClassicMapConstants.DistanceFromTheTop) / ClassicMapConstants.DistanceBetweenGameObjects;
                int col = (gameObject.Position.Left - ClassicMapConstants.DistanceFromTheLeft) / ClassicMapConstants.DistanceBetweenGameObjects;
                int minValue = int.MinValue;
                var movementType = MovementType.Up;
                bool isReflected = false;
                if (col >= ClassicMapConstants.BoardCols)
                {
                    isReflected = true;
                    col = col - ClassicMapConstants.BoardCols;
                    col = ClassicMapConstants.BoardCols - col - 1;
                }

                if (col + 1 < ClassicMapConstants.BoardCols)
                {
                    if (this.board[row, col + 1] != 0 && this.board[row, col + 1] > minValue)
                    {
                        if (isReflected)
                        {
                            movementType = MovementType.Left;
                        }
                        else
                        {
                            movementType = MovementType.Right;
                        }

                        minValue = this.board[row, col + 1];
                    }
                }

                if (col - 1 >= 0)
                {
                    if (this.board[row, col - 1] != 0 && this.board[row, col - 1] > minValue)
                    {
                        if (isReflected)
                        {
                            movementType = MovementType.Right;
                        }
                        else
                        {
                            movementType = MovementType.Left;
                        }

                        minValue = this.board[row, col - 1];
                    }
                }

                if (this.board[row + 1, col] != 0 && this.board[row + 1, col] > minValue)
                {
                    movementType = MovementType.Down;
                    minValue = this.board[row + 1, col];
                }

                if (this.board[row - 1, col] != 0 && this.board[row - 1, col] > minValue)
                {
                    movementType = MovementType.Up;
                    minValue = this.board[row - 1, col];
                }

                return movementType;
            }

            return gameObject.MovementType;
        }

        public MovementType FindWayToCave(IMoveable gameObject)
        {
            return pathToCage.FindPath(gameObject, board);
            if ((gameObject.Position.Top - ClassicMapConstants.DistanceFromTheTop) % ClassicMapConstants.DistanceBetweenGameObjects == 0 &&
                (gameObject.Position.Left - ClassicMapConstants.DistanceFromTheLeft) % ClassicMapConstants.DistanceBetweenGameObjects == 0)
            {
                int row = (gameObject.Position.Top - ClassicMapConstants.DistanceFromTheTop) / ClassicMapConstants.DistanceBetweenGameObjects;
                int col = (gameObject.Position.Left - ClassicMapConstants.DistanceFromTheLeft) / ClassicMapConstants.DistanceBetweenGameObjects;
                bool isReflected = false;
                if (col >= ClassicMapConstants.BoardCols)
                {
                    isReflected = true;
                    col = col - ClassicMapConstants.BoardCols;
                    col = ClassicMapConstants.BoardCols - col - 1;
                }

                int maxValue = int.MaxValue;
                var movementType = MovementType.Up;
                if (col + 1 < ClassicMapConstants.BoardCols)
                {
                    if (this.board[row, col + 1] != 0 && this.board[row, col + 1] < maxValue)
                    {
                        if (!isReflected)
                        {
                            movementType = MovementType.Right;
                        }
                        else
                        {
                            movementType = MovementType.Left;
                        }

                        maxValue = this.board[row, col + 1];
                    }
                }

                if (col - 1 >= 0)
                {
                    if (col - 1 >= 0)
                    {
                        if (this.board[row, col - 1] != 0 && this.board[row, col - 1] < maxValue)
                        {
                            if (!isReflected)
                            {
                                movementType = MovementType.Left;
                            }
                            else
                            {
                                movementType = MovementType.Right;
                            }

                            maxValue = this.board[row, col - 1];
                        }
                    }
                }

                if (this.board[row + 1, col] != 0 && this.board[row + 1, col] < maxValue)
                {
                    movementType = MovementType.Down;
                    maxValue = this.board[row + 1, col];
                }

                if (this.board[row - 1, col] != 0 && this.board[row - 1, col] < maxValue)
                {
                    movementType = MovementType.Up;
                    maxValue = this.board[row - 1, col];
                }

                return movementType;
            }

            return gameObject.MovementType;
        }

        private void InitBoard()
        {
            this.board = new int[ClassicMapConstants.BoardRows, ClassicMapConstants.BoardCols]
            {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            { 0, 24, 23, 22, 21, 20, 19, 20, 21, 22, 21, 20, 19, 0, },
            { 0, 23, 0, 0, 0, 0, 18, 0, 0, 0, 0, 0, 18, 0, },
            { 0, 22, 0, 0, 0, 0, 17, 0, 0, 0, 0, 0, 17, 0, },
            { 0, 21, 0, 0, 0, 0, 16, 0, 0, 0, 0, 0, 16, 0, },
            { 0, 20, 19, 18, 17, 16, 15, 14, 13, 12, 13, 14, 15, 16, },
            { 0, 21, 0, 0, 0, 0, 16, 0, 0, 11, 0, 0, 0, 0, },
            { 0, 22, 0, 0, 0, 0, 17, 0, 0, 10, 0, 0, 0, 0, },
            { 0, 23, 22, 21, 20, 19, 18, 0, 0, 9, 8, 7, 6, 0, },
            { 0, 0, 0, 0, 0, 0, 16, 0, 0, 0, 0, 0, 5, 0, },
            { 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 4, 0, },
            { 0, 0, 0, 0, 0, 0, 14, 0, 0, 6, 5, 4, 3, 2, },
            { 0, 0, 0, 0, 0, 0, 14, 0, 0, 7, 0, 0, 0, -1, },
            { 0, 0, 0, 0, 0, 0, 12, 0, 0, 8, 0, -4 , -3, -2, },
            { 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 0, -5, -4, -3, },
            { 0, 0, 0, 0, 0, 0, 13, 0, 0, 10, 0, -6, -5, -4, },
            { 0, 0, 0, 0, 0, 0, 14, 0, 0, 11, 0, 0, 0, 0, },
            { 0, 0, 0, 0, 0, 0, 15, 0, 0, 12, 13, 14, 15, 16, },
            { 0, 0, 0, 0, 0, 0, 16, 0, 0, 13, 0, 0, 0, 0, },
            { 0, 0, 0, 0, 0, 0, 17, 0, 0, 14, 0, 0, 0, 0, },
            { 0, 23, 22, 21, 20, 19, 18, 17, 16, 15, 16, 17, 18, 0, },
            { 0, 24, 0, 0, 0, 0, 19, 0, 0, 0, 0, 0, 19, 0, },
            { 0, 25, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0, 20, 0, },
            { 0, 26, 27, 28, 0, 0, 21, 22, 23, 24, 23, 22, 21, 22, },
            { 0, 0, 0, 29, 0, 0, 22, 0, 0, 25, 0, 0, 0, 0, },
            { 0, 0, 0, 28, 0, 0, 23, 0, 0, 26, 0, 0, 0, 0, },
            { 0, 29, 28, 27, 26, 25, 24, 0, 0, 27, 28, 29, 30, 0, },
            { 0, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 31, 0, },
            { 0, 31, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 32, 0, },
            { 0, 32, 33, 34, 35, 36, 37, 38, 37, 36, 35, 34, 33, 34, },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            };
        }
    }
}