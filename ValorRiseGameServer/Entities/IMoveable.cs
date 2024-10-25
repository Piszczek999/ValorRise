using System.Numerics;

namespace ValorRiseGameServer;

public interface IMoveable
{
    Vector2 Position { get; set; }
    Vector2 Destination { get; set; }
    bool IsCollidable { get; set; }
    public float Speed { get; set; }

    void UpdatePosition(double delta);
}