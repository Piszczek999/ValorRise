using System.Numerics;
using ValorRise.Enums;

namespace ValorRiseGameServer.Entities;

public class Slime : LivingEntity
{
    public Slime(Vector2 position) : base(EntityType.Slime, position)
    {

    }
}