namespace MMOLibrary.Client;

public class CharactersInfoResultEvent : EventArgs
{
    public byte Count { get; }
    public CharacterInfo[] Characters { get; }

    public CharactersInfoResultEvent(byte count, CharacterInfo[] characters)
    {
        Count = count;
        Characters = characters;
    }
}