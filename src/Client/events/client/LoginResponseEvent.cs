namespace MMOLibrary.Client;

public class LoginResponseEvent : EventArgs
{
    public LoginResult Result { get; }

    public LoginResponseEvent(LoginResult result)
    {
        Result = result;
    }
}