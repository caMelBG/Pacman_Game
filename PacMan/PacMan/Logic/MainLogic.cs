namespace PacMan.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Threading;

    using Maps;
    using Models.Abstract;
    using Models.Common;
    using Renderers;
    using SoundProvider;
    using Models.Players;

    public partial class Engine
    {
        private int BegginingTimeInterval = 100;
        private int NextRoundTimeInterval = 50;
        private const int RegularDotAward = 10;
        private const int HeavyDotAward = 50;
        private const int EnemeyAward = 200;
        private const int ChanceToSpawnFruit = 555;
        private const int TimerLength = 18;
        private const int EnemeySlowSpeed = 2;
        private const int EnemeyFastSpeed = 3;
        private const int ColisionDistance = 10;
        private const int DistanceBetweenGameObjects = 50;
        private const int DistanceBetweenEachEatenFruit = 30;
        private const int DistanceOfEatenFruitsFromTop = 636;
        private PacmanLife pacmanLife;
        private IMap gameMap;
        private IGameRenderer gameRenderer;
        private SoundProvider soundProvider;
        private Pacman pacman;
        private Gate gate;
        private Fruit fruit;
        private List<Fruit> eatenFruits;
        private HashSet<Position> regularDotPositions;
        private HashSet<Position> heavyDotPositions;
        private HashSet<Enemey> enemeys;
        private DispatcherTimer timer;
        private bool isGameStarted;
        private MovementType playerNextMove;
        private Score currentScore;
        private HighScore bestScore;
        private Award earnedAward;

        public Engine(IMap map, IGameRenderer renderer)
        {
            this.gameMap = map;
            this.gameRenderer = renderer;
            this.bestScore = new HighScore();
            this.isGameStarted = false;
            this.timer = new DispatcherTimer();
            this.soundProvider = new SoundProvider();
            this.gameRenderer.UIActionHappened += this.HandleUIActionHappened;
            this.StartGame();
        }

        public void InitGame()
        {
            this.pacmanLife = new PacmanLife();
            this.currentScore = new Score();
            this.eatenFruits = new List<Fruit>();
            this.InitGameObjects();
        }

        public void InitGameObjects()
        {
            this.soundProvider.BeginningPlay();
            this.regularDotPositions = new HashSet<Position>(this.gameMap.InitRegularCoins());
            this.heavyDotPositions = new HashSet<Position>(this.gameMap.InitHeavyCoins());
            this.gate = this.gameMap.InitGate();
            this.InitPlayers();
        }

        private void InitPlayers()
        {
            this.enemeys = new HashSet<Enemey>(this.gameMap.InitEnemeys());
            this.pacman = this.gameMap.InitPacMan();
            this.fruit = null;
        }

        public void StartGame()
        {
            this.timer.Interval = TimeSpan.FromMilliseconds(TimerLength);
            this.timer.Tick += this.GameLoop;
            this.timer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (this.isGameStarted)
            {
                this.UpdatePositions();
            }

            BegginingTimeInterval--;
            this.DrawGameObjects();
        }

        private void PlayEnemeySounds()
        {
            if (this.enemeys.Any(x => x.CanKill == true))
            {
                this.soundProvider.ScaredPlay();
                this.soundProvider.PacmanMovePause();
            }
            else
            {
                this.soundProvider.ScaredPause();
                this.soundProvider.PacmanMovePlay();
            }

            if (this.enemeys.Any(x => x.IsAlive == false))
            {
                this.soundProvider.RetraitPlay();
            }
            else
            {
                this.soundProvider.RetraitPause();
            }

            if (!this.pacman.IsAlive)
            {
                this.soundProvider.PacmanChompPause();
                this.soundProvider.PacmanMovePause();
                this.soundProvider.ScaredPause();
                this.soundProvider.RetraitPause();
            }
        }

        private void HandleUIActionHappened(object sender, KeyDownEventArgs _event)
        {
            if (!this.isGameStarted && (BegginingTimeInterval <= 0))
            {
                foreach (var enemey in this.enemeys)
                {
                    var position = new Position(enemey.Position.Left + 9, enemey.Position.Top);
                    enemey.Position = position;
                }

                this.isGameStarted = true;
            }

            switch (_event.MovementType)
            {
                case MovementType.Up:
                    this.playerNextMove = MovementType.Up;
                    break;
                case MovementType.Down:
                    this.playerNextMove = MovementType.Down;
                    break;
                case MovementType.Left:
                    this.playerNextMove = MovementType.Left;
                    break;
                case MovementType.Right:
                    this.playerNextMove = MovementType.Right;
                    break;
            }
        }
    }
}