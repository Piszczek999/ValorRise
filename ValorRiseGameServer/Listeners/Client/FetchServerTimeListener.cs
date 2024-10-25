using ValorRise.Packets.Play.Client;
using ValorRise.Packets.Play.Server;

namespace ValorRiseGameServer.Listeners;

public class FetchServerTimeListener
{
    [ClientPacketListener]
    public void FetchServerTime(FetchServerTimePacket packet, PlayerConnection connection)
    {
        var newPacket = new FetchServerTimeResponsePacket(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(), packet.ClientTimestamp);
        connection.SendPacket(newPacket);
    }
}