namespace MMOLibrary.Server;
using MongoDB.Bson;
using Riptide;

public class CharacterSelectRequestEvent : EventArgs
{
    public Connection Client { get; }
    public ObjectId CharacterId { get; }

    public CharacterSelectRequestEvent(Connection client, ObjectId characterId)
    {
        Client = client;
        CharacterId = characterId;
    }
}