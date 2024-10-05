using Riptide;

namespace ValorRise.Entities;

public interface ILivingEntity : IEntity
{
    public double Health { get; set; }
    public double MaxHealth { get; set; }
}