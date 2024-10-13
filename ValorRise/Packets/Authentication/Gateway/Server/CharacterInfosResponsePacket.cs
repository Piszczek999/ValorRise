using Riptide;
using ValorRise.Enums;
using ValorRise.Models;

namespace ValorRise.Packets.Authentication.Gateway;

[Packet(PacketType.CharacterInfosResponse, MessageSendMode.Reliable)]
public record CharacterInfosResponsePacket(CharacterInfo[] CharacterInfos) : IServerPacket
{
    public CharacterInfosResponsePacket(Message packet) : this(packet.GetSerializables<CharacterInfo>())
    {

    }

    public void Write(Message packet) => packet
        .AddSerializables(CharacterInfos);
}
