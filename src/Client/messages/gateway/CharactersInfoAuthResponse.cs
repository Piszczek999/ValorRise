namespace MMOLibrary.Client.Messages;
using Riptide;

internal class CharactersInfoAuthResponse : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharactersInfoAuthResponse(EventBus eventBus)
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

        var args = new CharactersInfoAuthResponseEvent(clientId, characters);
        _eventBus.Publish(args);
    }
}