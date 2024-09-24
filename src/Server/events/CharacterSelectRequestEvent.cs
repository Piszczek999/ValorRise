namespace MMO_Library.Server;
using MongoDB.Bson;

public class CharacterSelectRequestEvent : EventArgs
{
    public Connection Connection { get; }
    public ObjectId CharacterId { get; }

    public CharacterSelectRequestEvent(Connection connection, ObjectId characterId)
    {
        Connection = connection;
        CharacterId = characterId;
    }
}