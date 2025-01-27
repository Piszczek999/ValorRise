using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Server;

[Packet(PacketType.CharacterSelectAuthResponse, MessageSendMode.Reliable)]
public record CharacterSelectAuthResponsePacket(
    ushort ClientId,
    CharacterSelectResult Result,
    string Token = "",
    string HostAddress = "",
    byte MapId = 0) : IServerPacket
{
    public CharacterSelectAuthResponsePacket(Message packet) : this(
        packet.GetUShort(),
        (CharacterSelectResult)packet.GetByte(),
        packet.GetString(),
        packet.GetString(),
        packet.GetByte())
    { }

    public void Write(Message packet) => packet
        .AddUShort(ClientId)
        .AddByte((byte)Result)
        .AddString(Token)
        .AddString(HostAddress)
        .AddByte(MapId);
}
