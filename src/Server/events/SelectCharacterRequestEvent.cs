namespace MMO_Library.Server;
using MongoDB.Bson;

public class SelectCharacterRequestEvent : EventArgs
{
    public Connection Connection { get; }
    public ObjectId CharacterId { get; }

    public SelectCharacterRequestEvent(Connection connection, ObjectId characterId)
    {
        Connection = connection;
        CharacterId = characterId;
    }
}