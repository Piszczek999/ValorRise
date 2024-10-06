using Riptide;
using ValorRise;

namespace ValorRise.Client.Entities;

public class PlayerEntity : LivingEntity
{
    public override void Serialize(Message message)
    {
        base.Serialize(message);
    }

    public static new PlayerEntity Deserialize(Message message)
    {
        var player = new PlayerEntity
        {
            EntityType = EntityType.Player,
            Id = message.GetObjectId(),
            Name = message.GetString(),
            X = message.GetFloat(),
            Y = message.GetFloat(),
            Health = message.GetFloat(),
            MaxHealth = message.GetFloat(),
        };
        return player;
    }
}
