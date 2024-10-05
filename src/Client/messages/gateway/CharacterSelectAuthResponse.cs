using Riptide;

namespace ValorRise.Client.Messages;

internal class CharacterSelectAuthResponse : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharacterSelectAuthResponse(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        ushort clientId = message.GetUShort();
        string token = message.GetString();
        string ipAddress = message.GetString();

        var args = new CharacterSelectAuthResponseEvent(clientId, token, ipAddress);
        _eventBus.Publish(args);
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