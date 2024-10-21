using ValorRise;
using ValorRise.Packets.Authentication.GameServer;
using ValorRiseAuthenticate;
using ValorRiseAuthenticate.MongoDB;

namespace ValorRiseAuthenticate.Listeners;

public class CharacterLogoutListener
{
    [ClientPacketListener]
    public async void Listener(CharacterLogoutPacket packet, ClientConnection connection)
    {
        if (packet.Character != null)
        {
            await Database.CharacterRepository.UpdateAsync(packet.Character);
        }
        await Database.UserRepository.LogoutAsync(packet.Character.UserId);
    }
}
