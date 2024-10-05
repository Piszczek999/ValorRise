using Riptide;
using ValorRise;

namespace ValorRise.Client.Entities;

public class Player : LivingEntity
{
    public static new Player Deserialize(Message message)
    {
        var player = new Player
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