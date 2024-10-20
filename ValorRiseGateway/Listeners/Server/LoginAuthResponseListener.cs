using ValorRise.Enums;
using ValorRise.Packets.Authentication.Gateway;
using ValorRise.Packets.Authentication.Server;

namespace ValorRiseGateway.Listeners.Server;

public class LoginAuthResponseListener
{
    [ServerPacketListener]
    public void LoginAuthResponse(LoginAuthResponsePacket packet)
    {
        if (ValorServer.TryGetClient(packet.ClientId, out var connection))
        {
            if (packet.Result == LoginResult.Success)
            {
                connection.UserId = packet.UserId;
            }

            var newPacket = new LoginResponsePacket(packet.Result);
            connection.SendPacket(newPacket);
        }
    }
}
