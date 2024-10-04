using Riptide;

namespace MMOLibrary.Client;

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