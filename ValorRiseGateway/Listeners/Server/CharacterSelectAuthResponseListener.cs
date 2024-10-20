using MongoDB.Bson;
using ValorRise;
using ValorRise.Enums;
using ValorRise.Packets.Authentication.Gateway;
using ValorRise.Packets.Authentication.Server;

namespace ValorRiseGateway.Listeners.Server;

public class CharacterSelectAuthResponseListener
{
    [ServerPacketListener]
    public void CharacterSelectAuthResponse(CharacterSelectAuthResponsePacket packet)
    {
        if (!ValorServer.TryGetClient(packet.ClientId, out var connection)) return;
        Logger.Debug($"Result sent: {packet.Result}");
        Logger.Debug($"Token sent: {packet.Token}");
        Logger.Debug($"Host address sent: {packet.HostAddress}");

        if (packet.Result == CharacterSelectResult.Success)
        {
            Logger.Debug($"Success");
            connection.UserId = ObjectId.Empty; // clear user id to prevent logging out
            var newPacket = new CharacterSelectResponsePacket(packet.Result, packet.Token, packet.HostAddress);
            connection.SendPacket(newPacket);
            connection.Disconnect();
        }
        else
        {
            Logger.Debug($"Failed");
            var newPacket = new CharacterSelectResponsePacket(packet.Result);
            connection.SendPacket(newPacket);
        }
    }
}
