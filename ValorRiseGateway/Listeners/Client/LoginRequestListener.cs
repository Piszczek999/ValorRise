using ValorRise;
using ValorRise.Packets.Authentication.Client;
using ValorRise.Packets.Authentication.Gateway;

namespace ValorRiseGateway.Listeners.Client;

public class LoginRequestListener
{
    [ClientPacketListener]
    public void LoginRequest(LoginRequestPacket packet, ClientConnection connection)
    {
        var newPacket = new LoginAuthRequestPacket(connection.Id, packet.Username, packet.Password);
        ValorClient.Client.SendPacket(newPacket);
    }
}
