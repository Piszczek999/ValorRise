namespace MMOLibrary.Client;
using MongoDB.Bson;

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