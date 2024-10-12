using System.Numerics;

namespace ValorRiseServer;

public static class Vector2Extensions
{
    public static Vector2 Normalize(this Vector2 vector)
    {
        var magnitude = vector.Length();
        if (magnitude == 0)
            return vector;

        return vector /= magnitude;
    }
}