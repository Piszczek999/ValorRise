using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToGameServer.PlayerToken)]
internal class PlayerToken : IMessageHandler
{
    public void HandleMessage(Message message)
    {
        string token = message.GetString();
        Character character = message.GetCharacter();

        var args = new PlayerTokenEvent(token, character);
        MMOClient.EventBus.Publish(args);
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