namespace MMO_Library.Server;
using MongoDB.Bson;
using Riptide;

public class CharacterSelectAuthRequestEvent : EventArgs
{
    public Connection Gateway { get; }
    public ushort ClientId { get; }
    public ObjectId UserId { get; }
    public ObjectId CharacterId { get; }

    public CharacterSelectAuthRequestEvent(Connection gateway, ushort clientId, ObjectId userId, ObjectId characterId)
    {
        Gateway = gateway;
        ClientId = clientId;
        UserId = userId;
        CharacterId = characterId;
    }
}