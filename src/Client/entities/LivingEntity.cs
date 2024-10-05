using Riptide;
using ValorRise.Entities;

namespace ValorRiseClient;

public abstract class LivingEntity : Entity, ILivingEntity
{
    public double Health { get; set; }
    public double MaxHealth { get; set; }
}