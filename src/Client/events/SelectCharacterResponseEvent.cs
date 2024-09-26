namespace MMOLibrary.Client;

public class SelectCharacterResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public Character Character { get; }

    public SelectCharacterResponseEvent(ushort clientId, Character character)
    {
        ClientId = clientId;
        Character = character;
    }
}