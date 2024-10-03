namespace MMOLibrary.Server;
using MongoDB.Bson;
using Riptide;

public class CharactersInfoDBRequestEvent : EventArgs
{
    public Connection Authenticate { get; }
    public ushort GatewayId { get; }
    public ushort ClientId { get; }
    public ObjectId UserId { get; }

    public CharactersInfoDBRequestEvent(Connection authenticate, ushort gatewayId, ushort clientId, ObjectId userId)
    {
        Authenticate = authenticate;
        GatewayId = gatewayId;
        ClientId = clientId;
        UserId = userId;
    }
}