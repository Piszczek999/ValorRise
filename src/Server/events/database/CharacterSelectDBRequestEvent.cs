namespace MMOLibrary.Server;
using MongoDB.Bson;
using Riptide;

public class CharacterSelectDBRequestEvent : EventArgs
{
    public Connection Authenticate { get; }
    public ushort GatewayId { get; }
    public ushort ClientId { get; }
    public ObjectId UserId { get; }
    public ObjectId CharacterId { get; }

    public CharacterSelectDBRequestEvent(Connection authenticate, ushort gatewayId, ushort clientId, ObjectId userId, ObjectId characterId)
    {
        Authenticate = authenticate;
        GatewayId = gatewayId;
        ClientId = clientId;
        UserId = userId;
        CharacterId = characterId;
    }
}