using ValorRise;
using ValorRise.Packets.Play.Client;

namespace ValorRiseGameServer.Listeners;

public class PlayerMovementListener
{
    [ClientPacketListener]
    public void PlayerMoveClick(ClientPlayerMovementPacket packet, PlayerConnection connection)
    {
        var player = connection.Player;

        if (player == null) return;
        if (packet.Timestamp <= player.LastUpdateTimestamp) return;
        player.LastUpdateTimestamp = packet.Timestamp;
        if (player.Position == packet.Destination) return;

        player.Destination = packet.Destination;
    }
}