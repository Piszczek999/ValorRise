using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToGateway.CharacterSelectAuthResponse)]
internal class CharacterSelectAuthResponse : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOClient.GlobalEventHandler;

    public void HandleMessage(Message message)
    {
        ushort clientId = message.GetUShort();
        string token = message.GetString();
        string ipAddress = message.GetString();
        ushort port = message.GetUShort();

        var args = new CharacterSelectAuthResponseEvent(clientId, token, ipAddress, port);
        _eventHandler.InvokeEvent(args);
    }
}

public class CharacterSelectAuthResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public string Token { get; }
    public string IpAddress { get; }
    public ushort Port { get; }

    public CharacterSelectAuthResponseEvent(ushort clientId, string token, string ipAddress, ushort port)
    {
        ClientId = clientId;
        Token = token;
        IpAddress = ipAddress;
        Port = port;
    }
}