namespace MMOLibrary.Client;

public class NewCharacterResponseEvent : EventArgs
{
    public NewCharacterResult Result { get; }

    public NewCharacterResponseEvent(NewCharacterResult result)
    {
        Result = result;
    }
}