namespace MMOLibrary.Server.Messages;
using Riptide;

internal class RegisterRequest : IMessageHandler
{
  private readonly EventBus _eventBus;

  public RegisterRequest(EventBus eventBus)
  {
    _eventBus = eventBus;
  }

  public void HandleMessage(ushort clientId, Message message)
  {
    string username = message.GetString();
    string password = message.GetString();

    if (!MMOServer.TryGetClient(clientId, out var client)) throw new InvalidOperationException("Client not found for specified clientId");
    var args = new RegisterRequestEvent(client, username, password);
    _eventBus.Publish(args);
  }
}