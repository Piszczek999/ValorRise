namespace MMOLibrary.Client;
using MongoDB.Bson;
using Riptide;

internal class CharactersInfoResultMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharactersInfoResultMessageHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        byte count = message.GetByte();
        CharacterInfo[] characters = new CharacterInfo[count];
        for (int i = 0; i < count; i++)
        {
            characters[i] = message.GetCharacterInfo();
        }

        var args = new CharactersInfoResultEvent(count, characters);
        _eventBus.Publish(args);
    }
}