namespace MMOLibrary.Client.Messages;
using Riptide;

internal class SpawnEntity : IMessageHandler
{
    private readonly EventBus _eventBus;

    public SpawnEntity(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        Entity entity = Entity.Deserialize(message);

        var args = new SpawnEntityEvent(entity);
        _eventBus.Publish(args);
    }
}