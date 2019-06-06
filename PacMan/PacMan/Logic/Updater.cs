namespace PacMan.Logic
{
    using PacMan.Common;
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
                this.UpdateEnemeis();
                this.UpdateFruit();
                this.PlayEnemeiSounds();
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
            else if ((this.pacman.Position.Left - ClassicMapConstants.DistanceFromTheLeft) % ClassicMapConstants.DistanceBetweenGameObjects == 0)
            {
                if ((this.pacman.Position.Top - ClassicMapConstants.DistanceFromTheTop) % ClassicMapConstants.DistanceBetweenGameObjects == 0)
                {
                    this.soundProvider.PacmanChompPause();
                }
            }

            if (this.heavyDotPositions.Contains(this.pacman.Position))
            {
                foreach (var enemy in this.enemeis)
                {
                    this.currentScore.Value += HeavyDotAward;
                    enemy.CanKill = true;
                }

                this.heavyDotPositions.Remove(this.pacman.Position);
            }
        }

        private void UpdateEnemeis()
        {
            foreach (var enemy in this.enemeis)
            {
                if (enemy.CanKill)
                {
                    if (enemy.MoveSpeed != EnemySlowSpeed)
                    {
                        enemy.MoveSpeed = EnemySlowSpeed;
                        if (enemy.Position.Left % EnemySlowSpeed > 0)
                        {
                            enemy.Position = new Position(enemy.Position.Left - 1, enemy.Position.Top);
                        }

                        if (enemy.Position.Top % EnemySlowSpeed > 0)
                        {
                            enemy.Position = new Position(enemy.Position.Left, enemy.Position.Top - 1);
                        }
                    }

                    enemy.MovementType = this.GenerateMovementFarFromPacman(enemy);
                    if (this.IsPacmanHitGameObject(enemy))
                    {
                        enemy.IsAlive = false;
                        this.soundProvider.EatGhostPlay();
                        this.currentScore.Value += enemy.Award.Value;
                        this.earnedAward = enemy.Award;
                        this.earnedAward.Position = enemy.Position;
                        foreach (var aliveEnemy in this.enemeis)
                        {
                            if (aliveEnemy.IsAlive)
                            {
                                aliveEnemy.Award.Value += EnemyAward;
                            }
                        }

                        ///?TODO
                        Task.Delay(2000);
                    }
                }
                else if (!enemy.IsAlive)
                {
                    if (enemy.MoveSpeed != EnemyFastSpeed)
                    {
                        enemy.MoveSpeed = EnemyFastSpeed;
                        int left = ((enemy.Position.Left - ClassicMapConstants.DistanceFromTheLeft) % ClassicMapConstants.DistanceBetweenGameObjects) % EnemyFastSpeed;
                        int top = ((enemy.Position.Top - ClassicMapConstants.DistanceFromTheTop) % ClassicMapConstants.DistanceBetweenGameObjects) % EnemyFastSpeed;
                        enemy.Position = new Position(enemy.Position.Left - left, enemy.Position.Top - top);
                    }

                    if (this.IsEnemyEntryToTheCave(enemy))
                    {
                        enemy.IsAlive = true;
                        enemy.IsInCave = true;
                    }

                    enemy.MovementType = this.gameMap.FindWayToCave(enemy);
                }
                else if (enemy.IsInCave)
                {
                    if (this.IsEnemyEntryToTheCave(enemy))
                    {
                        enemy.IsInCave = false;
                    }

                    enemy.MovementType = this.gameMap.FindWayOutOfCave(enemy);
                }
                else
                {
                    if (this.IsPacmanHitGameObject(enemy))
                    {
                        this.soundProvider.PacmanDeathPlay();
                        this.pacmanLife.Count--;
                        this.pacman.IsAlive = false;
                    }

                    if (enemy.MoveSpeed != EnemyFastSpeed)
                    {
                        enemy.MoveSpeed = EnemyFastSpeed;
                        int left = ((enemy.Position.Left - ClassicMapConstants.DistanceFromTheLeft) % ClassicMapConstants.DistanceBetweenGameObjects) % EnemyFastSpeed;
                        int top = ((enemy.Position.Top - ClassicMapConstants.DistanceFromTheTop) % ClassicMapConstants.DistanceBetweenGameObjects) % EnemyFastSpeed;
                        enemy.Position = new Position(enemy.Position.Left - left, enemy.Position.Top - top);
                    }

                    enemy.Award.Value = EnemyAward;
                    enemy.MovementType = this.GenerateMovementNearToPacman(enemy);
                }

                enemy.MakeAMove();
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

                this.fruit.MovementType = this.GenerateEnemyMovement(this.fruit);
                this.fruit.MakeAMove();
            }
            else if (RandomNumberGenerator.Next(10000) < ChanceToSpawnFruit)
            {
                this.fruit = this.gameMap.InitFruit();
            }
        }

    }
}