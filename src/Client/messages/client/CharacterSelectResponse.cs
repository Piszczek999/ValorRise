using Riptide;

namespace ValorRise.Client.Messages;

internal class CharacterSelectResponse : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharacterSelectResponse(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        string token = message.GetString();
        string ipAddress = message.GetString();

        var args = new CharacterSelectResponseEvent(token, ipAddress);
        _eventBus.Publish(args);
    }
}

public class CharacterSelectResponseEvent : EventArgs
{
    public string Token { get; }
    public string IpAddress { get; }

    public CharacterSelectResponseEvent(string token, string ipAddress)
    {
        Token = token;
        IpAddress = ipAddress;
    }
}