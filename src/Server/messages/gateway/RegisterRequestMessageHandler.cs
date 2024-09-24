namespace MMO_Library.Server;
using Riptide;

internal class RegisterRequestMessageHandler : IMessageHandler
{
  private readonly EventBus _eventBus;
  private readonly ConnectionManager _clientManager;

  public RegisterRequestMessageHandler(EventBus eventBus, ConnectionManager clientManager)
  {
    _eventBus = eventBus;
    _clientManager = clientManager;
  }

  public void HandleMessage(ushort clientId, Message message)
  {
    string username = message.GetString();
    string password = message.GetString();

    var client = _clientManager.GetConnection(clientId);
    var args = new RegisterRequestEvent(client, username, password);
    _eventBus.Publish(args);
  }
}