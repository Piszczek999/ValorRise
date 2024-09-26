namespace MMOLibrary.Server;
using Riptide;

internal class LoginRequestMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;

    public LoginRequestMessageHandler(EventBus eventBus)
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