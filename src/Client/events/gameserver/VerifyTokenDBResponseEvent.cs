namespace MMOLibrary.Client;

public class VerifyTokenDBResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public Character Character { get; }

    public VerifyTokenDBResponseEvent(ushort clientId, Character character)
    {
        ClientId = clientId;
        Character = character;
    }
}