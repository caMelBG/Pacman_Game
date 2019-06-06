namespace PacMan.Logic
{
    using System;
    using System.Collections.Generic;
    using PacMan.Common;
    using PacMan.Maps;
    using PacMan.Models.Common;
    using PacMan.Models.Interfaces;

    public partial class Engine
    {
        private bool IsPacmanHitGameObject(IGameObject gameObject)
        {
            double x = Math.Pow(this.pacman.Position.Left - gameObject.Position.Left, 2);
            double y = Math.Pow(this.pacman.Position.Top - gameObject.Position.Top, 2);
            double sqrt = Math.Sqrt(x + y);
            return sqrt < ColisionDistance;
        }

        private bool IsEnemyEntryToTheCave(IGameObject gameObject)
        {
            var positio = new Position(this.gate.Position.Left, this.gate.Position.Top + ClassicMapConstants.DistanceBetweenGameObjects);
            double x = Math.Pow(this.gate.Position.Left - gameObject.Position.Left, 2);
            double y = Math.Pow(this.gate.Position.Top - gameObject.Position.Top + ClassicMapConstants.DistanceBetweenGameObjects, 2);
            double sqrt = Math.Sqrt(x + y);
            return sqrt < ColisionDistance;
        }

        private MovementType GenerateEnemyMovement(IMoveable gameObject)
        {
            var movements = new List<MovementType>();
            if (this.gameMap.CanMove(gameObject, MovementType.Up) && gameObject.MovementType != MovementType.Down)
            {
                movements.Add(MovementType.Up);
            }

            if (this.gameMap.CanMove(gameObject, MovementType.Down) && gameObject.MovementType != MovementType.Up)
            {
                movements.Add(MovementType.Down);
            }

            if (this.gameMap.CanMove(gameObject, MovementType.Right) && gameObject.MovementType != MovementType.Left)
            {
                movements.Add(MovementType.Right);
            }

            if (this.gameMap.CanMove(gameObject, MovementType.Left) && gameObject.MovementType != MovementType.Right)
            {
                movements.Add(MovementType.Left);
            }

            var movementsLength = movements.Count;
            if (movementsLength == 0)
            {
                return gameObject.MovementType;
            }
            else if (movementsLength == 1)
            {
                return movements[0];
            }
            else
            {
                return movements[RandomNumberGenerator.Next(movementsLength)];
            }
        }

        private MovementType GenerateMovementNearToPacman(IMoveable gameObject)
        {
            var movements = new List<MovementType>();
            if (this.gameMap.CanMove(gameObject, MovementType.Up))
            {
                movements.Add(MovementType.Up);
            }

            if (this.gameMap.CanMove(gameObject, MovementType.Down))
            {
                movements.Add(MovementType.Down);
            }

            if (this.gameMap.CanMove(gameObject, MovementType.Right))
            {
                movements.Add(MovementType.Right);
            }

            if (this.gameMap.CanMove(gameObject, MovementType.Left))
            {
                movements.Add(MovementType.Left);
            }

            MovementType bestMovement = MovementType.Up;
            double minDistance = double.MaxValue;
            foreach (var movement in movements)
            {
                var move = new Movement(movement);
                double x = Math.Pow(this.pacman.Position.Left - (gameObject.Position.Left + move.Left), 2);
                double y = Math.Pow(this.pacman.Position.Top - (gameObject.Position.Top + move.Top), 2);
                double sqrt = Math.Sqrt(x + y);
                if (sqrt < minDistance)
                {
                    minDistance = sqrt;
                    bestMovement = movement;
                }
            }

            if (minDistance < 50)
            {
                return bestMovement;
            }
            else
            {
                return this.GenerateEnemyMovement(gameObject);
            }
        }

        private MovementType GenerateMovementFarFromPacman(IMoveable gameObject)
        {
            var movementTypes = new List<MovementType>();
            if (this.gameMap.CanMove(gameObject, MovementType.Up))
            {
                movementTypes.Add(MovementType.Up);
            }

            if (this.gameMap.CanMove(gameObject, MovementType.Down))
            {
                movementTypes.Add(MovementType.Down);
            }

            if (this.gameMap.CanMove(gameObject, MovementType.Right))
            {
                movementTypes.Add(MovementType.Right);
            }

            if (this.gameMap.CanMove(gameObject, MovementType.Left))
            {
                movementTypes.Add(MovementType.Left);
            }

            MovementType bestMovement = MovementType.Up;
            double minDistance = double.MinValue;
            foreach (var movementType in movementTypes)
            {
                var movement = new Movement(movementType);
                double x = Math.Pow(this.pacman.Position.Left - (gameObject.Position.Left + movement.Left), 2);
                double y = Math.Pow(this.pacman.Position.Top - (gameObject.Position.Top + movement.Top), 2);
                double sqrt = Math.Sqrt(x + y);
                if (sqrt > minDistance)
                {
                    minDistance = sqrt;
                    bestMovement = movementType;
                }
            }

            if (minDistance < 50)
            {
                return bestMovement;
            }
            else
            {
                return this.GenerateEnemyMovement(gameObject);
            }
        }
    }
}