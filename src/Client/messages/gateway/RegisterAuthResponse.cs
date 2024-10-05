namespace ValorRiseClient.Messages;
using Riptide;
using ValorRise;

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

public class RegisterAuthResponseEvent : EventArgs
{
  public ushort ClientId { get; }
  public RegisterResult Result { get; }

  public RegisterAuthResponseEvent(ushort clientId, RegisterResult result)
  {
    ClientId = clientId;
    Result = result;
  }
}