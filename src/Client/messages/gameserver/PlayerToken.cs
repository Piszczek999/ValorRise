using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToGameServer.PlayerToken)]
internal class PlayerToken : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOClient.GlobalEventHandler;

    public void HandleMessage(Message message)
    {
        string token = message.GetString();
        var character = Character.Deserialize(message);

        var args = new PlayerTokenEvent(token, character);
        _eventHandler.InvokeEvent(args);
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