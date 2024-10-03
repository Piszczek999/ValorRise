namespace MMOLibrary.Client;

public class NewCharacterAuthResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public NewCharacterResult Result { get; }

    public NewCharacterAuthResponseEvent(ushort clientId, NewCharacterResult result)
    {
        ClientId = clientId;
        Result = result;
    }
}