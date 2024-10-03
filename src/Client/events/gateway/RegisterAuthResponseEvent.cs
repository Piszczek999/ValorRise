namespace MMOLibrary.Client;

public class RegisterAuthResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public RegisterResult Result { get; }

    public RegisterAuthResponseEvent(ushort clientId, RegisterResult result)
    {
        ClientId = clientId;
        Result = result;
    }
}