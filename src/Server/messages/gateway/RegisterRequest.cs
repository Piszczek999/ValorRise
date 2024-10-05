using Riptide;

namespace ValorRise.Server.Messages;

[Message((ushort)MessageType.ToGateway.RegisterRequest)]
internal class RegisterRequest : IMessageHandler
{
    public void HandleMessage(ushort clientId, Message message)
    {
        string username = message.GetString();
        string password = message.GetString();

        if (!MMOServer.TryGetClient(clientId, out var client)) throw new InvalidOperationException("Client not found for specified clientId");
        var args = new RegisterRequestEvent(client, username, password);
        MMOServer.EventBus.Publish(args);
    }
}

public class RegisterRequestEvent : EventArgs
{
    public Connection Client { get; }
    public string Username { get; }
    public string Password { get; }

    public RegisterRequestEvent(Connection client, string username, string password)
    {
        Client = client;
        Username = username;
        Password = password;
    }
}