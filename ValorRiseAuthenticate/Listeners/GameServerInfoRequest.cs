using ValorRise;
using ValorRise.Packets.Authentication.Server;
using ValorRise.Packets.Loading.Client;

namespace ValorRiseAuthenticate.Listeners;

public class GameServerInfoRequest
{
    [ClientPacketListener]
    public void Listener(GameServerInfoRequestPacket packet, ClientConnection connection)
    {
        if (ValorServer.GameServerManager.TryAddServer(connection, packet.IpAddress, out var serverInfo))
        {
            connection.SendPacket(new GameServerInfoResponsePacket((ushort)serverInfo.Map, serverInfo.Port));
            Logger.Debug($"GameServer: {packet.IpAddress}:{serverInfo.Port} assigned to mapId: {serverInfo.Map}");
        }
        else
        {
            connection.SendPacket(new GameServerInfoResponsePacket(default, 0));
            Logger.Warning($"Failed to assign game server to {packet.IpAddress}");
        }
    }
}
