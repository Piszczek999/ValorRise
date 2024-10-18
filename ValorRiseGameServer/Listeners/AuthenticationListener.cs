using ValorRise;
using ValorRise.Packets.Loading.Client;
using ValorRise.Packets.Loading.Server;
using ValorRiseGameServer.Entities;
using ValorRiseGameServer.Events;

namespace ValorRiseGameServer.Listeners;

public class AuthenticationListener
{
    [PacketListener]
    public void PlayerAuthenticateListener(AuthenticateRequestPacket packet, PlayerConnection connection)
    {
        try
        {
            var character = ValorServer.VerificationManager.VerifyToken(connection, packet.Token);
            var result = character != null;
            if (result)
            {
                var player = Player.FromCharacter(character, connection);

                var @event = new PlayerJoinEvent(player);
                ValorServer.GlobalEventNode.Invoke(@event);
                connection.Player = player;
                ValorServer.EntityManager.AddEntity(player);

                player.SendInfoPackets();
            }
            connection.SendPacket(new AuthenticateResponsePacket(result));
        }
        catch (Exception ex)
        {
            Logger.Error("Error in PlayerAuthenticateListener: " + ex);
        }
    }
}