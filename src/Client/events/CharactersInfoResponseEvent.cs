namespace MMOLibrary.Client;

public class CharactersInfoResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public byte Count { get; }
    public CharacterInfo[] Characters { get; }

    public CharactersInfoResponseEvent(ushort clientId, byte count, CharacterInfo[] characters)
    {
        ClientId = clientId;
        Count = count;
        Characters = characters;
    }
}