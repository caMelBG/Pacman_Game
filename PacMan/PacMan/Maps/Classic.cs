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

    public class Classic : GameObject, IMap, IImagable
    {
        public const int MapWidth = 506;
        public const int MapHeight = 560;
        public const int DistanceBetweenGameObjects = 18;
        public const int DistanceFromTheTop = 60;
        public const int DistanceFromTheLeft = 10;
        private const int BoardRows = 31;
        private const int BoardCols = 14;
        private readonly Position fruitStartPosition = new Position((DistanceBetweenGameObjects * 13) + (DistanceBetweenGameObjects / 2) + 1 + DistanceFromTheLeft, (DistanceBetweenGameObjects * 17) + DistanceFromTheTop);
        private readonly Position pacmanStartPosition = new Position((DistanceBetweenGameObjects * 13) + (DistanceBetweenGameObjects / 2) + DistanceFromTheLeft, (DistanceBetweenGameObjects * 23) + DistanceFromTheTop);
        private readonly Position pinkyStartPosition = new Position((DistanceBetweenGameObjects * 11) + (DistanceBetweenGameObjects / 2) + DistanceFromTheLeft, (DistanceBetweenGameObjects * 14) + DistanceFromTheTop);
        private readonly Position inkyStartPosition = new Position((DistanceBetweenGameObjects * 13) + (DistanceBetweenGameObjects / 2) + DistanceFromTheLeft, (DistanceBetweenGameObjects * 14) + DistanceFromTheTop);
        private readonly Position blinkyStartPosition = new Position((DistanceBetweenGameObjects * 13) + (DistanceBetweenGameObjects / 2) + DistanceFromTheLeft, (DistanceBetweenGameObjects * 11) + DistanceFromTheTop);
        private readonly Position clydeStartPosition = new Position((DistanceBetweenGameObjects * 15) + (DistanceBetweenGameObjects / 2) + DistanceFromTheLeft, (DistanceBetweenGameObjects * 14) + DistanceFromTheTop);

        private int[,] board;
        private Image image;
        private PlayerFactory playerFactory;
        private FruitFactory fruitFactory;

        public Classic() : base(new Position(MapWidth / 2, (MapHeight / 2) + (DistanceFromTheTop - DistanceFromTheLeft)), new Size(MapWidth, MapHeight))
        {
            this.InitBoard();
            this.playerFactory = new PlayerFactory();
            this.fruitFactory = new FruitFactory();
            this.image = ImageParser.Parse(GlobalConstants.ClassicMapImagePath);
        }

        public bool CanMove(IMoveable gameObject, MovementType movementType)
        {
            var movement = new Movement(movementType);
            int row = gameObject.Position.Top - DistanceFromTheTop;
            int col = gameObject.Position.Left - DistanceFromTheLeft;
            if (row % DistanceBetweenGameObjects == 0 && col % DistanceBetweenGameObjects == 0)
            {
                row /= DistanceBetweenGameObjects;
                col /= DistanceBetweenGameObjects;
                col += movement.Left;
                row += movement.Top;

                if (col >= BoardCols)
                {
                    col = col - BoardCols;
                    col = BoardCols - col - 1;
                }

                ///TELEPORT
                if (col < 0)
                {
                    if (gameObject is Models.Players.Pacman)
                    {
                        if (gameObject.Position.Left == DistanceFromTheLeft)
                        {
                            gameObject.Position = new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 28), gameObject.Position.Top);
                        }
                        else
                        {
                            gameObject.Position = new Position(DistanceFromTheLeft, gameObject.Position.Top);
                        }

                        return true;
                    }
                    else
                    {
                        if (gameObject.Position.Left == DistanceFromTheLeft)
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
            else if (row % DistanceBetweenGameObjects == 0)
            {
                if (movementType == MovementType.Left || movementType == MovementType.Right)
                {
                    return true;
                }
            }
            else if (col % DistanceBetweenGameObjects == 0)
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
            return new Gate(new Position((DistanceBetweenGameObjects * 13) + (DistanceBetweenGameObjects / 2) + DistanceFromTheLeft, (DistanceBetweenGameObjects * 12) + (DistanceBetweenGameObjects / 6) + DistanceFromTheTop));
        }

        public Fruit InitFruit()
        {
            switch (RandomNumberGenerator.Next(6))
            {
                case 0: return this.fruitFactory.CreatApple(this.fruitStartPosition);
                case 1: return this.fruitFactory.CreatCherry(this.fruitStartPosition);
                case 2: return this.fruitFactory.CreatGrapes(this.fruitStartPosition);
                case 3: return this.fruitFactory.CreatLemon(this.fruitStartPosition);
                case 4: return this.fruitFactory.CreatOrange(this.fruitStartPosition);
                case 5: return this.fruitFactory.CreatStrawberry(this.fruitStartPosition);
            }

            return this.fruitFactory.CreatApple(this.fruitStartPosition);
        }

        public Pacman InitPacMan()
        {
            return this.playerFactory.CreatPacman(this.pacmanStartPosition);
        }

        public IEnumerable<Enemey> InitEnemeys()
        {
            var enemeys = new List<Enemey>();
            enemeys.Add(this.playerFactory.CreatPinky(this.pinkyStartPosition, true));
            enemeys.Add(this.playerFactory.CreatInky(this.inkyStartPosition, true));
            enemeys.Add(this.playerFactory.CreatClyde(this.clydeStartPosition, true));
            enemeys.Add(this.playerFactory.CreatBlinky(this.blinkyStartPosition, false));
            return enemeys;
        }

        public IEnumerable<Position> InitRegularCoins()
        {
            var regularCoins = new List<Position>();
            ///12 points
            for (int index = 0; index < (12 * DistanceBetweenGameObjects); index += DistanceBetweenGameObjects)
            {
                regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 2) + index, DistanceFromTheTop + (DistanceBetweenGameObjects * 5)));
                regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 2) - index, DistanceFromTheTop + (DistanceBetweenGameObjects * 5)));
                regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 1) + index, DistanceFromTheTop + (DistanceBetweenGameObjects * 1)));
                regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 1) - index, DistanceFromTheTop + (DistanceBetweenGameObjects * 1)));
                regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 1) + index, DistanceFromTheTop + (DistanceBetweenGameObjects * 20)));
                regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 1) - index, DistanceFromTheTop + (DistanceBetweenGameObjects * 20)));
                regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 2) + index, DistanceFromTheTop + (DistanceBetweenGameObjects * 29)));
                regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 2) - index, DistanceFromTheTop + (DistanceBetweenGameObjects * 29)));

                ///11 points
                if (index < (11 * DistanceBetweenGameObjects))
                {
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 6), DistanceFromTheTop + (DistanceBetweenGameObjects * 9) + index));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 6), DistanceFromTheTop + (DistanceBetweenGameObjects * 9) + index));
                }

                ///6 points
                if (index < (6 * DistanceBetweenGameObjects))
                {
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 6), DistanceFromTheTop + (DistanceBetweenGameObjects * 21) + index));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 6), DistanceFromTheTop + (DistanceBetweenGameObjects * 21) + index));
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 7) + index, DistanceFromTheTop + (DistanceBetweenGameObjects * 23)));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 7) - index, DistanceFromTheTop + (DistanceBetweenGameObjects * 23)));
                }

                ///5 points
                if (index < (5 * DistanceBetweenGameObjects))
                {
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 1) + index, DistanceFromTheTop + (DistanceBetweenGameObjects * 26)));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 1) - index, DistanceFromTheTop + (DistanceBetweenGameObjects * 26)));
                }

                ///4 points
                if (index < (4 * DistanceBetweenGameObjects))
                {
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 2) + index, DistanceFromTheTop + (DistanceBetweenGameObjects * 8)));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 2) - index, DistanceFromTheTop + (DistanceBetweenGameObjects * 8)));
                }

                ///3 points
                if (index < (3 * DistanceBetweenGameObjects))
                {
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 1), DistanceFromTheTop + (DistanceBetweenGameObjects * 6) + index));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 1), DistanceFromTheTop + (DistanceBetweenGameObjects * 6) + index));
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 6), DistanceFromTheTop + (DistanceBetweenGameObjects * 2) + index));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 6), DistanceFromTheTop + (DistanceBetweenGameObjects * 2) + index));
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 12), DistanceFromTheTop + (DistanceBetweenGameObjects * 2) + index));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 12), DistanceFromTheTop + (DistanceBetweenGameObjects * 2) + index));
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 6), DistanceFromTheTop + (DistanceBetweenGameObjects * 6) + index));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 6), DistanceFromTheTop + (DistanceBetweenGameObjects * 6) + index));
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 9), DistanceFromTheTop + (DistanceBetweenGameObjects * 6) + index));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 9), DistanceFromTheTop + (DistanceBetweenGameObjects * 6) + index));
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 10) + index, DistanceFromTheTop + (DistanceBetweenGameObjects * 8)));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 10) - index, DistanceFromTheTop + (DistanceBetweenGameObjects * 8)));
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 1), DistanceFromTheTop + (DistanceBetweenGameObjects * 27) + index));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 1), DistanceFromTheTop + (DistanceBetweenGameObjects * 27) + index));
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 9), DistanceFromTheTop + (DistanceBetweenGameObjects * 24) + index));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 9), DistanceFromTheTop + (DistanceBetweenGameObjects * 24) + index));
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 12), DistanceFromTheTop + (DistanceBetweenGameObjects * 26) + index));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 12), DistanceFromTheTop + (DistanceBetweenGameObjects * 26) + index));
                }

                ///2 points
                if (index < (2 * DistanceBetweenGameObjects))
                {
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 1), DistanceFromTheTop + (DistanceBetweenGameObjects * 4) + index));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 1), DistanceFromTheTop + (DistanceBetweenGameObjects * 4) + index));
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 1), DistanceFromTheTop + (DistanceBetweenGameObjects * 21) + index));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 1), DistanceFromTheTop + (DistanceBetweenGameObjects * 21) + index));
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 3), DistanceFromTheTop + (DistanceBetweenGameObjects * 24) + index));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 3), DistanceFromTheTop + (DistanceBetweenGameObjects * 24) + index));
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 12), DistanceFromTheTop + (DistanceBetweenGameObjects * 21) + index));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 12), DistanceFromTheTop + (DistanceBetweenGameObjects * 21) + index));
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 2) + index, DistanceFromTheTop + (DistanceBetweenGameObjects * 23)));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 2) - index, DistanceFromTheTop + (DistanceBetweenGameObjects * 23)));
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 10) + index, DistanceFromTheTop + (DistanceBetweenGameObjects * 26)));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 10) - index, DistanceFromTheTop + (DistanceBetweenGameObjects * 26)));
                }

                ///1 point
                if (index < DistanceBetweenGameObjects)
                {
                    regularCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 1), DistanceFromTheTop + (DistanceBetweenGameObjects * 2) + index));
                    regularCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 1), DistanceFromTheTop + (DistanceBetweenGameObjects * 2) + index));
                }
            }

            return regularCoins;
        }

        public IEnumerable<Position> InitHeavyCoins()
        {
            var heavyCoins = new List<Position>();
            heavyCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 1), DistanceFromTheTop + (DistanceBetweenGameObjects * 3)));
            heavyCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 1), DistanceFromTheTop + (DistanceBetweenGameObjects * 3)));
            heavyCoins.Add(new Position(DistanceFromTheLeft + (DistanceBetweenGameObjects * 1), DistanceFromTheTop + (DistanceBetweenGameObjects * 23)));
            heavyCoins.Add(new Position(MapWidth - DistanceFromTheLeft - (DistanceBetweenGameObjects * 1), DistanceFromTheTop + (DistanceBetweenGameObjects * 23)));
            return heavyCoins;
        }

        public MovementType FindWayOutOfCave(IMoveable gameObject)
        {
            if ((gameObject.Position.Top - DistanceFromTheTop) % DistanceBetweenGameObjects == 0 &&
                (gameObject.Position.Left - DistanceFromTheLeft) % DistanceBetweenGameObjects == 0)
            {
                int row = (gameObject.Position.Top - DistanceFromTheTop) / DistanceBetweenGameObjects;
                int col = (gameObject.Position.Left - DistanceFromTheLeft) / DistanceBetweenGameObjects;
                int minValue = int.MinValue;
                var movementType = MovementType.Up;
                bool isReflected = false;
                if (col >= BoardCols)
                {
                    isReflected = true;
                    col = col - BoardCols;
                    col = BoardCols - col - 1;
                }

                if (col + 1 < BoardCols)
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
            if ((gameObject.Position.Top - DistanceFromTheTop) % DistanceBetweenGameObjects == 0 &&
                (gameObject.Position.Left - DistanceFromTheLeft) % DistanceBetweenGameObjects == 0)
            {
                int row = (gameObject.Position.Top - DistanceFromTheTop) / DistanceBetweenGameObjects;
                int col = (gameObject.Position.Left - DistanceFromTheLeft) / DistanceBetweenGameObjects;
                bool isReflected = false;
                if (col >= BoardCols)
                {
                    isReflected = true;
                    col = col - BoardCols;
                    col = BoardCols - col - 1;
                }

                int maxValue = int.MaxValue;
                var movementType = MovementType.Up;
                if (col + 1 < BoardCols)
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
            this.board = new int[BoardRows, BoardCols]
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