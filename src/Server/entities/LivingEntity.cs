namespace ValorRiseServer.Entities;
using Riptide;
using ValorRise.Entities;

public class LivingEntity : Entity, ILivingEntity
{
    public double Health { get; set; }
    public double MaxHealth { get; set; }

    public void TakeDamage(double damage)
    {
        Health -= damage;
        if (Health <= 0)
            Die();
    }

    public void Die()
    {
        // Implement death behavior
    }

    public void Heal(double healAmount)
    {
        Health = Math.Min(Health + healAmount, MaxHealth);
    }

    public override void Serialize(Message message)
    {
        base.Serialize(message);
        message.AddDouble(Health);
        message.AddDouble(MaxHealth);
    }
}