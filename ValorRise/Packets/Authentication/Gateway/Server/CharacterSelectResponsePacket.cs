using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Gateway;

[Packet(PacketType.CharacterSelectResponse, MessageSendMode.Reliable)]
public record CharacterSelectResponsePacket(
    CharacterSelectResult Result,
    string Token = "",
    string HostAddress = "",
    byte MapId = 0) : IServerPacket
{
    public CharacterSelectResponsePacket(Message packet) : this(
        (CharacterSelectResult)packet.GetByte(),
        packet.GetString(),
        packet.GetString(),
        packet.GetByte())
    { }

    public void Write(Message packet) => packet
        .AddByte((byte)Result)
        .AddString(Token)
        .AddString(HostAddress)
        .AddByte(MapId);
}
