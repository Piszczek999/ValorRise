using MongoDB.Bson;
using Riptide;

namespace MMO_Library.Server;

public class NewCharacterAuthRequestEvent : EventArgs
{
    public Connection Gateway { get; }
    public ushort ClientId { get; }
    public ObjectId UserId { get; }
    public string Name { get; }

    public NewCharacterAuthRequestEvent(Connection gateway, ushort clientId, ObjectId userId, string name)
    {
        Gateway = gateway;
        ClientId = clientId;
        UserId = userId;
        Name = name;
    }
}