using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;
using ValorRise.Models;

namespace ValorRise.Packets.Authentication.GameServer;

[Packet(PacketType.CharacterLogout, MessageSendMode.Reliable)]
public record CharacterLogoutPacket(Character Character) : IClientPacket
{
    public CharacterLogoutPacket(Message packet) : this(packet.GetSerializable<Character>())
    {

    }

    public void Write(Message packet) => packet
        .AddSerializable(Character);
}
