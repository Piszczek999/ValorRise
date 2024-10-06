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

    public override void Serialize(Message message)
    {
        base.Serialize(message);
        message.AddFloat(Health)
               .AddFloat(MaxHealth);
    }

    public override void Deserialize(Message message)
    {
        base.Deserialize(message);
        Health = message.GetFloat();
        MaxHealth = message.GetFloat();
    }
}