using Riptide;

namespace ValorRise.Client.Messages;

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
        var characters = message.GetCharacterInfos(3);

        var args = new CharactersInfoAuthResponseEvent(clientId, characters);
        _eventBus.Publish(args);
    }
}

public class CharactersInfoAuthResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public CharacterInfo[] Characters { get; }

    public CharactersInfoAuthResponseEvent(ushort clientId, CharacterInfo[] characters)
    {
        ClientId = clientId;
        Characters = characters;
    }
}