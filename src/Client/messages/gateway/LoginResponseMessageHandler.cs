namespace MMOLibrary.Client;
using MongoDB.Bson;
using Riptide;

internal class LoginResponseMessageHandler : IMessageHandler
{
  private readonly EventBus _eventBus;

  public LoginResponseMessageHandler(EventBus eventBus)
  {
    _eventBus = eventBus;
  }

  public void HandleMessage(Message message)
  {
    ushort clientId = message.GetUShort();
    LoginResult result = (LoginResult)message.GetByte();
    ObjectId userId = message.GetObjectId();

    var args = new LoginResponseEvent(clientId, result, userId);
    _eventBus.Publish(args);
  }
}