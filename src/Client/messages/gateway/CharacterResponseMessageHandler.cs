namespace MMOLibrary.Client;
using Riptide;

internal class CharacterResponseMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharacterResponseMessageHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        ushort clientId = message.GetUShort();
        var character = message.GetCharacter();

        var args = new CharacterResponseEvent(clientId, character);
        _eventBus.Publish(args);
    }
}