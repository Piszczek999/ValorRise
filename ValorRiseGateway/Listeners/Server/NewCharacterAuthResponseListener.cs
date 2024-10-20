using ValorRise.Packets.Authentication.Gateway;
using ValorRise.Packets.Authentication.Server;

namespace ValorRiseGateway.Listeners.Server;

public class NewCharacterAuthResponseListener
{
    [ServerPacketListener]
    public void NewCharacterAuthResponse(NewCharacterAuthResponsePacket packet)
    {
        if (ValorServer.TryGetClient(packet.ClientId, out var connection))
        {
            var newPacket = new NewCharacterResponsePacket(packet.Result);
            connection.SendPacket(newPacket);
        }
    }
}
