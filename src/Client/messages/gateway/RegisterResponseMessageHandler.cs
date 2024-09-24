namespace MMO_Library.Client;
using Riptide;

internal class RegisterResponseMessageHandler : IMessageHandler
{
  private readonly EventBus _eventBus;

  public RegisterResponseMessageHandler(EventBus eventBus)
  {
    _eventBus = eventBus;
  }

  public void HandleMessage(Message message)
  {
    ushort clientId = message.GetUShort();
    RegisterResult result = (RegisterResult)message.GetByte();

    var args = new RegisterResponseEvent(clientId, result);
    _eventBus.Publish(args);
  }
}