namespace MMOLibrary.Client;

public class VerifyTokenResponseEvent : EventArgs
{
    public bool Result { get; set; }

    public VerifyTokenResponseEvent(bool result)
    {
        Result = result;
    }
}