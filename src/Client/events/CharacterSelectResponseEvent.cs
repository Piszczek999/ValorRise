namespace MMOLibrary.Client;

public class CharacterSelectResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public Character Character { get; }

    public CharacterSelectResponseEvent(ushort clientId, Character character)
    {
        ClientId = clientId;
        Character = character;
    }
}