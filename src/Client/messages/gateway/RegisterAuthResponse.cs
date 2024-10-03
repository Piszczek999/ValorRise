namespace MMOLibrary.Client.Messages;
using Riptide;

internal class RegisterAuthResponse : IMessageHandler
{
  private readonly EventBus _eventBus;

  public RegisterAuthResponse(EventBus eventBus)
  {
    _eventBus = eventBus;
  }

  public void HandleMessage(Message message)
  {
    ushort clientId = message.GetUShort();
    RegisterResult result = (RegisterResult)message.GetByte();

    var args = new RegisterAuthResponseEvent(clientId, result);
    _eventBus.Publish(args);
  }
}