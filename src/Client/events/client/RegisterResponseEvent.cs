namespace MMOLibrary.Client;

public class RegisterResponseEvent : EventArgs
{
    public RegisterResult Result { get; }

    public RegisterResponseEvent(RegisterResult result)
    {
        Result = result;
    }
}