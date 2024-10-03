namespace MMOLibrary.Client;

public class CharacterSelectDBResponseEvent : EventArgs
{
    public ushort GatewayId { get; }
    public ushort ClientId { get; }
    public string Token { get; }
    public string IpAddress { get; }

    public CharacterSelectDBResponseEvent(ushort gatewayId, ushort clientId, string token, string ipAddress)
    {
        GatewayId = gatewayId;
        ClientId = clientId;
        Token = token;
        IpAddress = ipAddress;
    }
}