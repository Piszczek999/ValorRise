using Riptide;

namespace ValorRise.Server.Messages;

[Message((ushort)MessageType.ToGateway.RegisterRequest)]
internal class RegisterRequest : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOServer.GlobalEventHandler;

    public void HandleMessage(ushort clientId, Message message)
    {
        string username = message.GetString();
        string password = message.GetString();

        var args = new RegisterRequestEvent(clientId, username, password);
        _eventHandler.InvokeEvent(args);
    }
}

public class RegisterRequestEvent : EventArgs
{
    public ushort ClientId { get; }
    public Connection Client { get => MMOServer.TryGetClient(ClientId, out var client) ? client : null; }
    public string Username { get; }
    public string Password { get; }

    public RegisterRequestEvent(ushort clientId, string username, string password)
    {
        ClientId = clientId;
        Username = username;
        Password = password;
    }
}