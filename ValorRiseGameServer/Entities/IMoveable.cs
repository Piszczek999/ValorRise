using System.Numerics;

namespace ValorRiseGameServer;

public interface IMoveable
{
    Vector2 Position { get; }
    Vector2 Destination { get; }
    bool IsCollidable { get; }
    float Speed { get; }
    bool IsMoving { get; }
}