namespace MMOLibrary.Client;
using Riptide;

internal class CharacterSelectResponseMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharacterSelectResponseMessageHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        ushort clientId = message.GetUShort();
        Character character = message.GetCharacter();

        var args = new CharacterSelectResponseEvent(clientId, character);
        _eventBus.Publish(args);
    }
}