namespace MMOLibrary.Client;

public class CharactersInfoResponseEvent : EventArgs
{
    public CharacterInfo[] Characters { get; }

    public CharactersInfoResponseEvent(CharacterInfo[] characters)
    {
        Characters = characters;
    }
}