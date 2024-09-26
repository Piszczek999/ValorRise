namespace MMOLibrary.Client;

public class RegisterResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public RegisterResult Result { get; }

    public RegisterResponseEvent(ushort clientId, RegisterResult result)
    {
        ClientId = clientId;
        Result = result;
    }
}