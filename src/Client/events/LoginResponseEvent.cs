namespace MMO_Library.Client;
using MongoDB.Bson;

public class LoginResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public LoginResult Result { get; }
    public ObjectId UserId { get; }

    public LoginResponseEvent(ushort clientId, LoginResult result, ObjectId userId)
    {
        ClientId = clientId;
        Result = result;
        UserId = userId;
    }
}