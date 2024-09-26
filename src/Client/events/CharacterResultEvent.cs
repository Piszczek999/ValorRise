namespace MMOLibrary.Client;

public class CharacterResultEvent : EventArgs
{
    public Character Character { get; }

    public CharacterResultEvent(Character character)
    {
        Character = character;
    }
}