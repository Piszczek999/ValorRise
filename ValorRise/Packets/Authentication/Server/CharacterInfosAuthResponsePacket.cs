using Riptide;
using ValorRise.Enums;
using ValorRise.Models;

namespace ValorRise.Packets.Authentication.Server;

[Packet(PacketType.CharacterInfosAuthResponse, MessageSendMode.Reliable)]
public record CharacterInfosAuthResponsePacket(ushort ClientId, CharacterInfo[] CharacterInfos) : IServerPacket
{
    public CharacterInfosAuthResponsePacket(Message buffer) : this(
        buffer.GetUShort(),
        buffer.GetSerializables<CharacterInfo>())
    { }

    public void Write(Message buffer) => buffer
        .AddUShort(ClientId)
        .AddSerializables(CharacterInfos);
}
