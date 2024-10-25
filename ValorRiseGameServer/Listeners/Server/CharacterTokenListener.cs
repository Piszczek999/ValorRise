using ValorRise;
using ValorRise.Packets.Authentication.Server;

namespace ValorRiseGameServer.Listeners.Server;

public class CharacterTokenListener
{
    [ServerPacketListener]
    public void CharacterToken(CharacterTokenPacket packet)
    {
        ValorServer.VerificationManager.InitToken(packet.Token, packet.Character);
    }
}
