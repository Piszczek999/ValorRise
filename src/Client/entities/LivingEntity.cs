using Riptide;

namespace ValorRise.Client.Entities;

public abstract class LivingEntity : Entity
{
    public float Health { get; set; }
    public float MaxHealth { get; set; }

    public override void Serialize(Message message)
    {
        base.Serialize(message);
        message.AddFloat(Health)
               .AddFloat(MaxHealth);
    }
}