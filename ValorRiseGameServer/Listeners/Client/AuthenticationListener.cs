using ValorRise;
using ValorRise.Packets.Loading.Client;
using ValorRise.Packets.Loading.Server;
using ValorRise.Packets.Play.Server;
using ValorRiseGameServer.Entities;
using ValorRiseGameServer.Events;

namespace ValorRiseGameServer.Listeners;

public class AuthenticationListener
{
    [ClientPacketListener]
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

                player.SendSpawnInfoPackets();
                ValorServer.SendToAll(new SpawnEntityPacket(player.Id, player.EntityType, player.Position, player.Destination), connection.ConnectionId);
            }
            else
            {
                connection.Disconnect();
            }

            connection.SendPacket(new AuthenticateResponsePacket(result));
        }
        catch (Exception ex)
        {
            Logger.Error("Error in PlayerAuthenticateListener: " + ex);
        }
    }
}