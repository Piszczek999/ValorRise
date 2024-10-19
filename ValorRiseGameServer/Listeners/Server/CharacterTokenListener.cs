using ValorRise;
using ValorRise.Packets.Authentication.Server;

namespace ValorRiseGameServer.Listeners.Server;

public class CharacterTokenListener
{
    [ServerPacketListener]
    public void Listener(CharacterTokenPacket packet)
    {
        ValorServer.VerificationManager.InitToken(packet.Token, packet.Character);
    }
}
