namespace MMOLibrary.Client;

public class LoginResultEvent : EventArgs
{
    public LoginResult Result { get; }

    public LoginResultEvent(LoginResult result)
    {
        Result = result;
    }
}