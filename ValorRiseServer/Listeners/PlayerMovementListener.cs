using ValorRise.Packets.Play.Client;

namespace ValorRiseServer;

public class PlayerMovementListener
{
    [PacketListener(typeof(ClientPlayerMovementPacket))]
    public void ClientPlayerMovementListener(ClientPlayerMovementPacket message, PlayerConnection connection)
    {
        connection.Player.Destination = message.Destination;
    }
}