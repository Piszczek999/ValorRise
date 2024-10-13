using ValorRise.Packets.Play.Client;

namespace ValorRiseServer;

public class AuthenticationListener
{
    [PacketListener]
    public void PlayerAuthenticateListener(ClientPlayerAuthenticatePacket packet, PlayerConnection connection)
    {
        var player = ValorServer.VerificationManager.VerifyToken(connection, packet.Token);
        if (player != null)
        {
            var @event = new PlayerJoinEvent(player);
            ValorServer.GlobalEventNode.Invoke(@event);

            connection.Player = player;
            ValorServer.EntityManager.AddEntity(@event.Player);
        }
    }
}