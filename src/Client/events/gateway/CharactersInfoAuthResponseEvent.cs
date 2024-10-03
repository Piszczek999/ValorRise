namespace MMOLibrary.Client;

public class CharactersInfoAuthResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public CharacterInfo[] Characters { get; }

    public CharactersInfoAuthResponseEvent(ushort clientId, CharacterInfo[] characters)
    {
        ClientId = clientId;
        Characters = characters;
    }
}