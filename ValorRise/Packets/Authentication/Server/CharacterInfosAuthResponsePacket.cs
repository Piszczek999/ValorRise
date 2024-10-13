using Riptide;
using ValorRise.Enums;
using ValorRise.Models;

namespace ValorRise.Packets.Authentication.Server;

[Packet(PacketType.CharacterInfosAuthResponse, MessageSendMode.Reliable)]
public record CharacterInfosAuthResponsePacket(ushort ClientId, CharacterInfo[] CharacterInfos) : IServerPacket
{
    public CharacterInfosAuthResponsePacket(Message packet) : this(packet.GetUShort(), packet.GetSerializables<CharacterInfo>())
    {

    }

    public void Write(Message packet) => packet
        .AddUShort(ClientId)
        .AddSerializables(CharacterInfos);
}
