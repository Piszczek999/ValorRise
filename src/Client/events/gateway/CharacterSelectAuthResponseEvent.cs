namespace MMOLibrary.Client;

public class CharacterSelectAuthResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public string Token { get; }
    public string IpAddress { get; }

    public CharacterSelectAuthResponseEvent(ushort clientId, string token, string ipAddress)
    {
        ClientId = clientId;
        Token = token;
        IpAddress = ipAddress;
    }
}