namespace ValorRiseGameServer;

public abstract class Updatable
{
    public virtual void Update(double delta) { }
    public virtual void PhysicsUpdate(double delta) { }
}