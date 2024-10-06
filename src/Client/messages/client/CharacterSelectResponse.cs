using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToClient.CharacterSelectResponse)]
internal class CharacterSelectResponse : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOClient.GlobalEventHandler;

    public void HandleMessage(Message message)
    {
        string token = message.GetString();
        string ipAddress = message.GetString();
        ushort port = message.GetUShort();

        var args = new CharacterSelectResponseEvent(token, ipAddress, port);
        _eventHandler.InvokeEvent(args);
    }
}

public class CharacterSelectResponseEvent : EventArgs
{
    public string Token { get; }
    public string IpAddress { get; }
    public ushort Port { get; }

    public CharacterSelectResponseEvent(string token, string ipAddress, ushort port)
    {
        Token = token;
        IpAddress = ipAddress;
        Port = port;
    }
}