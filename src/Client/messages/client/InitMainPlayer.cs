using Riptide;

namespace ValorRise.Client.Messages;

internal class InitMainPlayer : IMessageHandler
{
    private readonly EventBus _eventBus;

    public InitMainPlayer(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        Character character = message.GetCharacter();

        var args = new InitMainPlayerEvent(character);
        _eventBus.Publish(args);
    }
}

public class InitMainPlayerEvent : EventArgs
{
    public Character Character { get; }

    public InitMainPlayerEvent(Character character)
    {
        Character = character;
    }
}