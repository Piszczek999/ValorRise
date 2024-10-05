using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToGateway.CharacterSelectAuthResponse)]
internal class CharacterSelectAuthResponse : IMessageHandler
{
    public void HandleMessage(Message message)
    {
        ushort clientId = message.GetUShort();
        string token = message.GetString();
        string ipAddress = message.GetString();

        var args = new CharacterSelectAuthResponseEvent(clientId, token, ipAddress);
        MMOClient.EventBus.Publish(args);
    }
}

public class CharacterSelectAuthResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public string Token { get; }
    public string IpAddress { get; }

    public CharacterSelectAuthResponseEvent(ushort clientId, string token, string ipAddress)
    {
        ClientId = clientId;
        Token = token;
        IpAddress = ipAddress;
    }
}