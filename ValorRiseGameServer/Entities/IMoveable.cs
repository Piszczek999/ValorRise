using System.Numerics;

namespace ValorRiseGameServer;

public interface IMoveable
{
    Vector2 Destination { get; set; }
    bool IsCollidable { get; set; }
    public float Speed { get; set; }

    bool UpdatePosition(double delta);
}