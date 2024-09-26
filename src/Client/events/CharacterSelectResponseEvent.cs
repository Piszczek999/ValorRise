namespace MMOLibrary.Client;

public class CharacterSelectResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public CharacterSelectResult Result { get; }

    public CharacterSelectResponseEvent(ushort clientId, CharacterSelectResult result)
    {
        ClientId = clientId;
        Result = result;
    }
}