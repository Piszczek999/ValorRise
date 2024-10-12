using ValorRise.Packets.Play.Client;

namespace ValorRiseServer;

public class AuthenticationListener
{
    [PacketListener(typeof(ClientPlayerAuthenticatePacket))]
    public void PlayerAuthenticateListener(ClientPlayerAuthenticatePacket packet, PlayerConnection connection)
    {
        var player = ValorServer.VerificationManager.VerifyToken(connection, packet.Token);
        if (player != null)
        {
            connection.Player = player;
            ValorServer.EntityManager.AddEntity(player);
            ValorServer.GlobalEventNode.Invoke(new PlayerJoinEvent(player));
        }
    }
}