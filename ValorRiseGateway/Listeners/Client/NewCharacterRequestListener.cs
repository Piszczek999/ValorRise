using MongoDB.Bson;
using ValorRise;
using ValorRise.Packets.Authentication.Client;
using ValorRise.Packets.Authentication.Gateway;

namespace ValorRiseGateway.Listeners.Client;

public class NewCharacterRequestListener
{
    [ClientPacketListener]
    public void NewCharacterRequest(NewCharacterRequestPacket packet, ClientConnection connection)
    {
        if (connection.UserId == ObjectId.Empty)
        {
            Logger.Warning($"There is no userId for connection: {connection.Id}");
            return;
        }
        var newPacket = new NewCharacterAuthRequestPacket(connection.Id, connection.UserId, packet.Name);
        ValorClient.Client.SendPacket(newPacket);
    }
}
