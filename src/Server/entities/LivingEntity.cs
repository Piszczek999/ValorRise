using Riptide;

namespace ValorRise.Server.Entities;

public class LivingEntity : Entity
{
    public float Health { get; set; }
    public float MaxHealth { get; set; }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            Die();
    }

    public void Die()
    {
        // Implement death behavior
    }

    public void Heal(float healAmount)
    {
        Health = Math.Min(Health + healAmount, MaxHealth);
    }

    public override Message Serialize(Message message)
    {
        return base.Serialize(message)
        .AddFloat(Health)
        .AddFloat(MaxHealth);
    }
}