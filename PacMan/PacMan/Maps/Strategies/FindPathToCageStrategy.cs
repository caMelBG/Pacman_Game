using PacMan.Common;
using PacMan.Models.Common;
using PacMan.Models.Interfaces;
using System;

namespace PacMan.Maps.Strategies
{
    public class FindPathToCageStrategy : IPathfinderStrategy
    {
        public MovementType FindPath(IMoveable gameObject, int[,] board)
        {
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
                    if (board[row, col + 1] != 0 && board[row, col + 1] < maxValue)
                    {
                        if (!isReflected)
                        {
                            movementType = MovementType.Right;
                        }
                        else
                        {
                            movementType = MovementType.Left;
                        }

                        maxValue = board[row, col + 1];
                    }
                }

                if (col - 1 >= 0)
                {
                    if (col - 1 >= 0)
                    {
                        if (board[row, col - 1] != 0 && board[row, col - 1] < maxValue)
                        {
                            if (!isReflected)
                            {
                                movementType = MovementType.Left;
                            }
                            else
                            {
                                movementType = MovementType.Right;
                            }

                            maxValue = board[row, col - 1];
                        }
                    }
                }

                if (board[row + 1, col] != 0 && board[row + 1, col] < maxValue)
                {
                    movementType = MovementType.Down;
                    maxValue = board[row + 1, col];
                }

                if (board[row - 1, col] != 0 && board[row - 1, col] < maxValue)
                {
                    movementType = MovementType.Up;
                    maxValue = board[row - 1, col];
                }

                return movementType;
            }

            return gameObject.MovementType;
        }
    }
}
