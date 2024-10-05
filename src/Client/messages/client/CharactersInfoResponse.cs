using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToClient.CharactersInfoResponse)]
internal class CharactersInfoResponse : IMessageHandler
{
    public void HandleMessage(Message message)
    {
        var characterinfos = message.GetCharacterInfos(3);

        var args = new CharactersInfoResponseEvent(characterinfos);
        MMOClient.EventBus.Publish(args);
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