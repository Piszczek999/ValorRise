namespace MMO_Library.Server;

public class CharactersInfoEvent : EventArgs
{
    public Connection Connection { get; }
    public byte Count { get; }
    public CharacterInfo[] Characters { get; }

    public CharactersInfoEvent(Connection client, byte count, CharacterInfo[] characters)
    {
        Connection = client;
        Count = count;
        Characters = characters;
    }
}