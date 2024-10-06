using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToGateway.CharactersInfoAuthResponse)]
internal class CharactersInfoAuthResponse : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOClient.GlobalEventHandler;

    public void HandleMessage(Message message)
    {
        ushort clientId = message.GetUShort();
        var count = message.GetByte();
        var characters = message.GetCharacterInfos(count);

        var args = new CharactersInfoAuthResponseEvent(clientId, characters);
        _eventHandler.InvokeEvent(args);
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