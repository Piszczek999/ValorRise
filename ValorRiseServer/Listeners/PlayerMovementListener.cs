using ValorRise.Packets.Play.Client;

namespace ValorRiseServer;

public class PlayerMovementListener
{
    [PacketListener]
    public void ClientPlayerMovementListener(ClientPlayerMovementPacket packet, PlayerConnection connection)
    {
        connection.Player.Destination = packet.Destination;
        ValorServer.GlobalEventNode.Invoke(new PlayerMoveClickEvent(connection.Player, packet.Destination));
    }
}