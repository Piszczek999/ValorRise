namespace MMOLibrary.Server;
using Riptide;

public class VerifyTokenDBRequestEvent : EventArgs
{
    public Connection Gameserver { get; }
    public ushort ClientId { get; }
    public string Token { get; }

    public VerifyTokenDBRequestEvent(Connection gameserver, ushort clientId, string token)
    {
        Gameserver = gameserver;
        ClientId = clientId;
        Token = token;
    }
}