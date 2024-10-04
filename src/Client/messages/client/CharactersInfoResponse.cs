namespace MMOLibrary.Client.Messages;
using Riptide;

internal class CharactersInfoResponse : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharactersInfoResponse(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        var characterinfos = message.GetCharacterInfos(3);

        var args = new CharactersInfoResponseEvent(characterinfos);
        _eventBus.Publish(args);
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