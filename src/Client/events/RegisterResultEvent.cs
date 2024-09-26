namespace MMOLibrary.Client;

public class RegisterResultEvent : EventArgs
{
    public RegisterResult Result { get; }

    public RegisterResultEvent(RegisterResult result)
    {
        Result = result;
    }
}