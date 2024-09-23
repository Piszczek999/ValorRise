namespace MMO_Library.Client;
using Riptide;

internal class SelectCharacterResponseMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;

    public SelectCharacterResponseMessageHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        ushort clientId = message.GetUShort();
        Character character = message.GetCharacter();

        var args = new SelectCharacterResponseEvent(clientId, character);
        _eventBus.Publish(args);
    }
}