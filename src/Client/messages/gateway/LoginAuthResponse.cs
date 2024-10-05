using MongoDB.Bson;
using Riptide;

namespace ValorRise.Client.Messages;

internal class LoginAuthResponse : IMessageHandler
{
  private readonly EventBus _eventBus;

  public LoginAuthResponse(EventBus eventBus)
  {
    _eventBus = eventBus;
  }

  public void HandleMessage(Message message)
  {
    ushort clientId = message.GetUShort();
    LoginResult result = (LoginResult)message.GetByte();
    ObjectId userId = message.GetObjectId();

    var args = new LoginAuthResponseEvent(clientId, result, userId);
    _eventBus.Publish(args);
  }
}

public class LoginAuthResponseEvent : EventArgs
{
  public ushort ClientId { get; }
  public LoginResult Result { get; }
  public ObjectId UserId { get; }

  public LoginAuthResponseEvent(ushort clientId, LoginResult result, ObjectId userId)
  {
    ClientId = clientId;
    Result = result;
    UserId = userId;
  }
}