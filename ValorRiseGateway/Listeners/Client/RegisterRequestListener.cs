using ValorRise.Packets.Authentication.Client;
using ValorRise.Packets.Authentication.Gateway;

namespace ValorRiseGateway.Listeners.Client;

public class RegisterRequestListener
{
    [ClientPacketListener]
    public void RegisterRequest(RegisterRequestPacket packet, ClientConnection connection)
    {
        var newPacket = new RegisterAuthRequestPacket(connection.Id, packet.Username, packet.Password);
        ValorClient.Client.SendPacket(newPacket);
    }
}
