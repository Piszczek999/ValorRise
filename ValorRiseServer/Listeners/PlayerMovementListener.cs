using ValorRise.Packets.Play.Client;

namespace ValorRiseServer;

public class PlayerMovementListener
{
    [PacketListener]
    public void ClientPlayerMovementListener(ClientPlayerMovementPacket packet, PlayerConnection connection)
    {
        var player = connection.Player;
        if (player.Position == packet.Destination)
        {
            return;
        }

        var @event = new PlayerMoveClickEvent(connection.Player, packet.Destination);
        ValorServer.GlobalEventNode.Invoke(@event);

        if (@event.IsCancelled)
        {
            return;
        }

        connection.Player.Destination = @event.ClickPosition;
    }
}