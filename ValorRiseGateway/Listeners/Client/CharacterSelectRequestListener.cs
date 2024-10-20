using MongoDB.Bson;
using ValorRise;
using ValorRise.Packets.Authentication.Client;
using ValorRise.Packets.Authentication.Gateway;

namespace ValorRiseGateway.Listeners.Client;

public class CharacterSelectRequestListener
{
    [ClientPacketListener]
    public void CharacterSelectRequest(CharacterSelectRequestPacket packet, ClientConnection connection)
    {
        if (connection.UserId == ObjectId.Empty)
        {
            Logger.Warning($"There is no userId for connection: {connection.Id}");
            return;
        }
        var newPacket = new CharacterSelectAuthRequestPacket(connection.Id, connection.UserId, packet.CharacterId);
        ValorClient.Client.SendPacket(newPacket);
    }
}
