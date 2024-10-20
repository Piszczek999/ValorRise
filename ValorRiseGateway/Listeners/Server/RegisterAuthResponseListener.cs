using ValorRise.Packets.Authentication.Gateway;
using ValorRise.Packets.Authentication.Server;

namespace ValorRiseGateway.Listeners.Server;

public class RegisterAuthResponseListener
{
    [ServerPacketListener]
    public void RegisterAuthResponse(RegisterAuthResponsePacket packet)
    {
        if (ValorServer.TryGetClient(packet.ClientId, out var connection))
        {
            var newPacket = new RegisterResponsePacket(packet.Result);
            connection.SendPacket(newPacket);
        }
    }
}
