using ValorRise.Packets.Play.Client;
using ValorRiseGameServer.Events;

namespace ValorRiseGameServer.Listeners;

public class PlayerMovementListener
{
    [PacketListener]
    public void PlayerMoveClick(ClientPlayerMovementPacket packet, PlayerConnection connection)
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