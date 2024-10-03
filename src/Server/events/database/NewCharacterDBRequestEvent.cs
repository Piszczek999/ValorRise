namespace MMOLibrary.Server;

using MongoDB.Bson;
using Riptide;

public class NewCharacterDBRequestEvent : EventArgs
{
    public Connection Authenticate { get; }
    public ushort GatewayId { get; }
    public ushort ClientId { get; }
    public ObjectId UserId { get; }
    public string Name { get; }

    public NewCharacterDBRequestEvent(Connection authenticate, ushort gatewayId, ushort clientId, ObjectId userId, string name)
    {
        Authenticate = authenticate;
        GatewayId = gatewayId;
        ClientId = clientId;
        UserId = userId;
        Name = name;
    }
}