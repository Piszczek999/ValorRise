using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToClient.CharactersInfoResponse)]
internal class CharactersInfoResponse : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOClient.GlobalEventHandler;

    public void HandleMessage(Message message)
    {
        var characterinfos = message.GetSerializables<CharacterInfo>();

        var args = new CharactersInfoResponseEvent(characterinfos);
        _eventHandler.InvokeEvent(args);
    }
}

public class CharactersInfoResponseEvent : EventArgs
{
    public CharacterInfo[] Characters { get; }

    public CharactersInfoResponseEvent(CharacterInfo[] characters)
    {
        Characters = characters;
    }
}