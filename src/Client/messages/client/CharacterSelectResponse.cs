using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToClient.CharacterSelectResponse)]
internal class CharacterSelectResponse : IMessageHandler
{
    public void HandleMessage(Message message)
    {
        string token = message.GetString();
        string ipAddress = message.GetString();

        var args = new CharacterSelectResponseEvent(token, ipAddress);
        MMOClient.EventBus.Publish(args);
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