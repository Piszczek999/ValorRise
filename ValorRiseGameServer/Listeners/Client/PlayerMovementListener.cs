using ValorRise;
using ValorRise.Packets.Play.Client;

namespace ValorRiseGameServer.Listeners;

public class PlayerMovementListener
{
    [ClientPacketListener]
    public void PlayerMoveClick(ClientPlayerMovementPacket packet, PlayerConnection connection)
    {
        try
        {
            var player = connection.Player;
            Logger.Debug($"{player.Position} - {packet.Destination}");
            if (player.Position == packet.Destination)
            {
                return;
            }

            connection.Player.Destination = packet.Destination;
        }
        catch (Exception ex)
        {
            Logger.Error($"Error processing PlayerMoveClick packet: {ex.Message}");
        }
    }
}