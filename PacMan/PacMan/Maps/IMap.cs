namespace PacMan.Maps
{
    using System.Collections.Generic;

    using Models.Abstract;
    using Models.Common;
    using Models.Interfaces;
    using Models.Players;
    using PacMan.Maps.Strategies;

    public interface IMap : IImagable
    {
        Pacman InitPacMan();

        Gate InitGate();

        Fruit InitFruit();
            
        IEnumerable<Enemy> InitEnemies();

        IEnumerable<Position> InitRegularCoins();

        IEnumerable<Position> InitHeavyCoins();

        //MovementType FindPath(IMoveable gameObject, IPathfinderStrategy strategy);

        MovementType FindWayOutOfCave(IMoveable gameObject);

        MovementType FindWayToCave(IMoveable gameObject);

        bool CanMove(IMoveable gameObject, MovementType movementType);
    }
}