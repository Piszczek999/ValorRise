namespace ValorRise.Client.Entities;

public abstract class LivingEntity : Entity
{
    public double Health { get; set; }
    public double MaxHealth { get; set; }
}