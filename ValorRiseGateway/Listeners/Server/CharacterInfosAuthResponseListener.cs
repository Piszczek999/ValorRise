using ValorRise.Packets.Authentication.Gateway;
using ValorRise.Packets.Authentication.Server;

namespace ValorRiseGateway.Listeners.Server;

public class CharacterInfosAuthResponseListener
{
    [ServerPacketListener]
    public void CharacterInfosAuthResponse(CharacterInfosAuthResponsePacket packet)
    {
        if (ValorServer.TryGetClient(packet.ClientId, out var connection))
        {
            var newPacket = new CharacterInfosResponsePacket(packet.CharacterInfos);
            connection.SendPacket(newPacket);
        }
    }
}
