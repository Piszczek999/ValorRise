namespace MMOLibrary.Client;

public class CharacterSelectResponseEvent : EventArgs
{
    public string Token { get; }
    public string IpAddress { get; }

    public CharacterSelectResponseEvent(string token, string ipAddress)
    {
        Token = token;
        IpAddress = ipAddress;
    }
}