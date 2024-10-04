namespace MMOLibrary.Client;

public class SpawnEntityEvent : EventArgs
{
    public Entity Entity { get; }

    public SpawnEntityEvent(Entity entity)
    {
        Entity = entity;
    }
}