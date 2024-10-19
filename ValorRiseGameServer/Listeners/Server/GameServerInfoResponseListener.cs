using ValorRise;
using ValorRise.Packets.Authentication.Server;

namespace ValorRiseGameServer.Listeners.Server;

public class GameServerInfoResponseListener
{
    [ServerPacketListener]
    public void Listener(GameServerInfoResponsePacket packet)
    {
        if (packet.Port != 0)
        {
            ValorServer.Server.Start(packet.Port, 500);
            // Init map here
        }
        else
        {
            Logger.Warning($"No free maps available");
        }
    }
}
