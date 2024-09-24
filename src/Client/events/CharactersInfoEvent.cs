namespace MMO_Library.Client;

public class CharactersInfoEvent : EventArgs
{
    public ushort ClientId { get; }
    public byte Count { get; }
    public CharacterInfo[] Characters { get; }

    public CharactersInfoEvent(ushort clientId, byte count, CharacterInfo[] characters)
    {
        ClientId = clientId;
        Count = count;
        Characters = characters;
    }
}