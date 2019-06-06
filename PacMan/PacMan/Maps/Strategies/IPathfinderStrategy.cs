using PacMan.Models.Common;
using PacMan.Models.Interfaces;

namespace PacMan.Maps.Strategies
{
    public interface IPathfinderStrategy
    {
        MovementType FindPath(IMoveable gameObject, int[,] board);
    }
}
