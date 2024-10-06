using Riptide;
using ValorRise;

namespace ValorRise.Client.Entities;

public class PlayerEntity : LivingEntity
{
    public static new PlayerEntity Deserialize(Message message)
    {
        var player = new PlayerEntity
        {
            EntityType = EntityType.Player,
            Id = message.GetObjectId(),
            Name = message.GetString(),
            Position = message.GetVector2(),
            Health = message.GetDouble(),
            MaxHealth = message.GetDouble(),
        };
        return player;
    }
}