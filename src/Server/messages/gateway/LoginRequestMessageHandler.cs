namespace MMO_Library.Server;
using Riptide;

internal class LoginRequestMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;
    private readonly Server _server;

    public LoginRequestMessageHandler(EventBus eventBus, Server server)
    {
        _eventBus = eventBus;
        _server = server;
    }

    public void HandleMessage(ushort clientId, Message message)
    {
        string username = message.GetString();
        string password = message.GetString();

        if (!_server.TryGetClient(clientId, out var client)) throw new InvalidOperationException("Client not found for specified clientId");
        var args = new LoginRequestEvent(client, username, password);
        _eventBus.Publish(args);
    }
}