using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToClient.InitMainPlayer)]
internal class InitMainPlayer : IMessageHandler
{
    public void HandleMessage(Message message)
    {
        Character character = message.GetCharacter();

        var args = new InitMainPlayerEvent(character);
        MMOClient.EventBus.Publish(args);
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