namespace PacMan.Logic
{
    using PacMan.Maps;
    using PacMan.Models.Common;
    using System.Threading.Tasks;

    public partial class Engine
    {
        private void UpdatePositions()
        {
            if (this.pacman.IsAlive)
            {
                this.UpdateCoins();
                this.UpdatePacman();
                this.UpdateEnemeys();
                this.UpdateFruit();
                this.PlayEnemeySounds();
            }
            else
            {
                if (NextRoundTimeInterval <= 0)
                {
                    NextRoundTimeInterval = 50;
                    this.isGameStarted = false;
                    if (this.pacmanLife.Count < 0)
                    {
                        this.InitGame();
                    }
                    else
                    {
                        this.InitPlayers();
                    }
                }
                else
                {
                    NextRoundTimeInterval--;
                }
            }
        }

        private void UpdateCoins()
        {
            if (this.regularDotPositions.Contains(this.pacman.Position))
            {
                this.currentScore.Value += RegularDotAward;
                this.soundProvider.PacmanChompPlay();
                this.regularDotPositions.Remove(this.pacman.Position);
            }
            else if ((this.pacman.Position.Left - Classic.DistanceFromTheLeft) % Classic.DistanceBetweenGameObjects == 0)
            {
                if ((this.pacman.Position.Top - Classic.DistanceFromTheTop) % Classic.DistanceBetweenGameObjects == 0)
                {
                    this.soundProvider.PacmanChompPause();
                }
            }

            if (this.heavyDotPositions.Contains(this.pacman.Position))
            {
                foreach (var enemey in this.enemeys)
                {
                    this.currentScore.Value += HeavyDotAward;
                    enemey.CanKill = true;
                }

                this.heavyDotPositions.Remove(this.pacman.Position);
            }
        }

        private void UpdateEnemeys()
        {
            foreach (var enemey in this.enemeys)
            {
                if (enemey.CanKill)
                {
                    if (enemey.MoveSpeed != EnemeySlowSpeed)
                    {
                        enemey.MoveSpeed = EnemeySlowSpeed;
                        if (enemey.Position.Left % EnemeySlowSpeed > 0)
                        {
                            enemey.Position = new Position(enemey.Position.Left - 1, enemey.Position.Top);
                        }

                        if (enemey.Position.Top % EnemeySlowSpeed > 0)
                        {
                            enemey.Position = new Position(enemey.Position.Left, enemey.Position.Top - 1);
                        }
                    }

                    enemey.MovementType = this.GenerateMovementFarFromPacman(enemey);
                    if (this.IsPacmanHitGameObject(enemey))
                    {
                        enemey.IsAlive = false;
                        this.soundProvider.EatGhostPlay();
                        this.currentScore.Value += enemey.Award.Value;
                        this.earnedAward = enemey.Award;
                        this.earnedAward.Position = enemey.Position;
                        foreach (var aliveEnemey in this.enemeys)
                        {
                            if (aliveEnemey.IsAlive)
                            {
                                aliveEnemey.Award.Value += EnemeyAward;
                            }
                        }

                        ///?TODO
                        Task.Delay(2000);
                    }
                }
                else if (!enemey.IsAlive)
                {
                    if (enemey.MoveSpeed != EnemeyFastSpeed)
                    {
                        enemey.MoveSpeed = EnemeyFastSpeed;
                        int left = ((enemey.Position.Left - Classic.DistanceFromTheLeft) % Classic.DistanceBetweenGameObjects) % EnemeyFastSpeed;
                        int top = ((enemey.Position.Top - Classic.DistanceFromTheTop) % Classic.DistanceBetweenGameObjects) % EnemeyFastSpeed;
                        enemey.Position = new Position(enemey.Position.Left - left, enemey.Position.Top - top);
                    }

                    if (this.IsEnemeyEntryToTheCave(enemey))
                    {
                        enemey.IsAlive = true;
                        enemey.IsInCave = true;
                    }

                    enemey.MovementType = this.gameMap.FindWayToCave(enemey);
                }
                else if (enemey.IsInCave)
                {
                    if (this.IsEnemeyEntryToTheCave(enemey))
                    {
                        enemey.IsInCave = false;
                    }

                    enemey.MovementType = this.gameMap.FindWayOutOfCave(enemey);
                }
                else
                {
                    if (this.IsPacmanHitGameObject(enemey))
                    {
                        this.soundProvider.PacmanDeathPlay();
                        this.pacmanLife.Count--;
                        this.pacman.IsAlive = false;
                    }

                    if (enemey.MoveSpeed != EnemeyFastSpeed)
                    {
                        enemey.MoveSpeed = EnemeyFastSpeed;
                        int left = ((enemey.Position.Left - Classic.DistanceFromTheLeft) % Classic.DistanceBetweenGameObjects) % EnemeyFastSpeed;
                        int top = ((enemey.Position.Top - Classic.DistanceFromTheTop) % Classic.DistanceBetweenGameObjects) % EnemeyFastSpeed;
                        enemey.Position = new Position(enemey.Position.Left - left, enemey.Position.Top - top);
                    }

                    enemey.Award.Value = EnemeyAward;
                    enemey.MovementType = this.GenerateMovementNearToPacman(enemey);
                }

                enemey.MakeAMove();
            }
        }

        private void UpdatePacman()
        {
            if (this.gameMap.CanMove(this.pacman, this.playerNextMove))
            {
                this.pacman.MovementType = this.playerNextMove;
            }

            if (this.gameMap.CanMove(this.pacman, this.pacman.MovementType))
            {
                this.pacman.MakeAMove();
            }

            ///UPDATE SCORE
            if (bestScore.Value < currentScore.Value)
            {
                bestScore.Value = currentScore.Value;
                bestScore.Save();
            }
        }

        private void UpdateFruit()
        {
            if (this.fruit != null)
            {
                if (this.fruit.IsJumped)
                {
                    this.soundProvider.FruitStepPlay();
                }

                if (this.IsPacmanHitGameObject(this.fruit))
                {
                    this.currentScore.Value += this.fruit.Award.Value;
                    this.earnedAward = this.fruit.Award;
                    this.earnedAward.Position = this.fruit.Position;
                    this.soundProvider.EatFruitPlay();
                    this.eatenFruits.Add(this.fruit);
                    this.fruit = null;
                    return;
                }

                this.fruit.MovementType = this.GenerateEnemeyMovement(this.fruit);
                this.fruit.MakeAMove();
            }
            else if (RandomNumberGenerator.Next(10000) < ChanceToSpawnFruit)
            {
                this.fruit = this.gameMap.InitFruit();
            }
        }

    }
}