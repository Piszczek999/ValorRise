using System.Numerics;

namespace ValorRiseServer;

public interface IMoveable
{
    Vector2 Destination { get; set; }
    bool IsCollidable { get; set; }

    bool UpdatePosition(double delta);
}