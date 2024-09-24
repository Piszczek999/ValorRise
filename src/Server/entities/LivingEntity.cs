public class LivingEntity : Entity
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
}