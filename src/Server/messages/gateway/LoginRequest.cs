using Riptide;

namespace ValorRise.Server.Messages;

[Message((ushort)MessageType.ToGateway.LoginRequest)]
internal class LoginRequest : IMessageHandler
{
    public void HandleMessage(ushort clientId, Message message)
    {
        string username = message.GetString();
        string password = message.GetString();

        if (!MMOServer.TryGetClient(clientId, out var client)) throw new InvalidOperationException("Client not found for specified clientId");
        var args = new LoginRequestEvent(client, username, password);
        MMOServer.EventBus.Publish(args);
    }
}

public class LoginRequestEvent : EventArgs
{
    public Connection Client { get; }
    public string Username { get; }
    public string Password { get; }

    public LoginRequestEvent(Connection client, string username, string password)
    {
        Client = client;
        Username = username;
        Password = password;
    }
}
