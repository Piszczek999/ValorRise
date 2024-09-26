namespace MMOLibrary.Client;

public class NewCharacterResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public NewCharacterResult Result { get; }

    public NewCharacterResponseEvent(ushort clientId, NewCharacterResult result)
    {
        ClientId = clientId;
        Result = result;
    }
}