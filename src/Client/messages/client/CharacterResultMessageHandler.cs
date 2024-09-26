namespace MMOLibrary.Client;
using MongoDB.Bson;
using Riptide;

internal class CharacterResultMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharacterResultMessageHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        var character = message.GetCharacter();

        var args = new CharacterResultEvent(character);
        _eventBus.Publish(args);
    }
}