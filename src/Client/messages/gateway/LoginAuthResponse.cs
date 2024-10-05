using MongoDB.Bson;
using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToGateway.LoginAuthResponse)]
internal class LoginAuthResponse : IMessageHandler
{
  public void HandleMessage(Message message)
  {
    ushort clientId = message.GetUShort();
    LoginResult result = (LoginResult)message.GetByte();
    ObjectId userId = message.GetObjectId();

    var args = new LoginAuthResponseEvent(clientId, result, userId);
    MMOClient.EventBus.Publish(args);
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