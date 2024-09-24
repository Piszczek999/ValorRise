namespace MMO_Library.Client;

public class TokenVerificationResultEvent : EventArgs
{
    public bool Result { get; set; }

    public TokenVerificationResultEvent(bool result)
    {
        Result = result;
    }
}