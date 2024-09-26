namespace MMOLibrary.Client;

public class CharacterResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public Character Character { get; }

    public CharacterResponseEvent(ushort clientId, Character character)
    {
        ClientId = clientId;
        Character = character;
    }
}