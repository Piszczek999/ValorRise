using MongoDB.Bson;
using ValorRise;
using ValorRise.Packets.Authentication.GameServer;
using ValorRiseAuthenticate;
using ValorRiseAuthenticate.MongoDB;

namespace ValorRiseAuthenticate.Listeners;

public class UserLogout
{
    [ClientPacketListener]
    public async void Listener(UserLogoutPacket packet, ClientConnection connection)
    {
        if (packet.UserId != ObjectId.Empty)
        {
            await Database.UserRepository.LogoutAsync(packet.UserId);
        }
    }
}
