namespace PacMan.Models.Interfaces
{
    using PacMan.Models.Common;

    public interface IAwardable
    {
        Award Award { get; set; }
    }
}