namespace MMOLibrary.Server.Messages;
using Riptide;

internal class LoginRequest : IMessageHandler
{
    private readonly EventBus _eventBus;

    public LoginRequest(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(ushort clientId, Message message)
    {
        string username = message.GetString();
        string password = message.GetString();

        if (!MMOServer.TryGetClient(clientId, out var client)) throw new InvalidOperationException("Client not found for specified clientId");
        var args = new LoginRequestEvent(client, username, password);
        _eventBus.Publish(args);
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
