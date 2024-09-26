namespace MMOLibrary.Client;

public class NewCharacterResultEvent : EventArgs
{
    public NewCharacterResult Result { get; }

    public NewCharacterResultEvent(NewCharacterResult result)
    {
        Result = result;
    }
}