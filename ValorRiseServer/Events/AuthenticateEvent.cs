using ValorRiseServer.Entities;

namespace ValorRiseServer;

public class AuthenticateEvent : IPlayerEvent
{
    public PlayerConnection Connection { get; }
    public string Token { get; }

    public Player Player => Connection.Player;
    public Entity Entity => Player;

    public AuthenticateEvent(PlayerConnection connection, string token)
    {
        Connection = connection;
        Token = token;
    }
}