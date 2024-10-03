using MongoDB.Bson;

namespace MMOLibrary.Client;

public class LoginDBResponseEvent : EventArgs
{
    public ushort GatewayId { get; }
    public ushort ClientId { get; }
    public LoginResult Result { get; }
    public ObjectId UserId { get; }

    public LoginDBResponseEvent(ushort gatewayId, ushort clientId, LoginResult result, ObjectId userId)
    {
        GatewayId = gatewayId;
        ClientId = clientId;
        Result = result;
        UserId = userId;
    }
}