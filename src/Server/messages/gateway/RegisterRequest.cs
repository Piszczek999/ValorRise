using Riptide;

namespace ValorRise.Server.Messages;

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