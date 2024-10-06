namespace ValorRise.Client.Entities;

public abstract class LivingEntity : Entity
{
    public float Health { get; set; }
    public float MaxHealth { get; set; }
}