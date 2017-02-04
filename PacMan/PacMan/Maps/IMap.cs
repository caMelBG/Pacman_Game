namespace PacMan.Maps
{
    using System.Collections.Generic;

    using Models.Abstract;
    using Models.Common;
    using Models.Interfaces;
    using Models.Players;
    public interface IMap : IImagable
    {
        Pacman InitPacMan();

        Gate InitGate();

        Fruit InitFruit();
            
        IEnumerable<Enemey> InitEnemeys();

        IEnumerable<Position> InitRegularCoins();

        IEnumerable<Position> InitHeavyCoins();

        MovementType FindWayOutOfCave(IMoveable gameObject);

        MovementType FindWayToCave(IMoveable gameObject);
        
        bool CanMove(IMoveable gameObject, MovementType movementType);
    }
}