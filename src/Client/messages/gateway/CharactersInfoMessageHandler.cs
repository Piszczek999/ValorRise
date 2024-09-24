namespace MMO_Library.Client;

using Riptide;

internal class CharactersInfoMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharactersInfoMessageHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        ushort clientId = message.GetUShort();
        byte count = message.GetByte();
        CharacterInfo[] characters = new CharacterInfo[count];
        for (int i = 0; i < count; i++)
        {
            characters[i] = message.GetCharacterInfo();
        }

        var args = new CharactersInfoEvent(clientId, count, characters);
        _eventBus.Publish(args);
    }
}