namespace ValorRiseClient.Messages;
using Riptide;
using ValorRise;

internal class PlayerToken : IMessageHandler
{
    private readonly EventBus _eventBus;

    public PlayerToken(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        string token = message.GetString();
        Character character = message.GetCharacter();

        var args = new PlayerTokenEvent(token, character);
        _eventBus.Publish(args);
    }
}

public class PlayerTokenEvent : EventArgs
{
    public string Token { get; }
    public Character Character { get; }

    public PlayerTokenEvent(string token, Character character)
    {
        Token = token;
        Character = character;
    }
}