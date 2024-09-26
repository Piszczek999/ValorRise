namespace MMOLibrary.Client;

using Riptide;

internal class CharactersInfoResponseMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharactersInfoResponseMessageHandler(EventBus eventBus)
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