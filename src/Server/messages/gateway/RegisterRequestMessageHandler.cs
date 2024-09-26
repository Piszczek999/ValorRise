namespace MMOLibrary.Server;
using Riptide;

internal class RegisterRequestMessageHandler : IMessageHandler
{
  private readonly EventBus _eventBus;
  private readonly MMOServer _server;

  public RegisterRequestMessageHandler(EventBus eventBus, MMOServer server)
  {
    _eventBus = eventBus;
    _server = server;
  }

  public void HandleMessage(ushort clientId, Message message)
  {
    string username = message.GetString();
    string password = message.GetString();

    if (!_server.TryGetClient(clientId, out var client)) throw new InvalidOperationException("Client not found for specified clientId");
    var args = new RegisterRequestEvent(client, username, password);
    _eventBus.Publish(args);
  }
}