using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Gateway;

[Packet(PacketType.CharacterSelectResponse, MessageSendMode.Reliable)]
public record CharacterSelectResponsePacket(CharacterSelectResult Result, string Token = "", string HostAddress = "") : IServerPacket
{
    public CharacterSelectResponsePacket(Message packet) : this(
        (CharacterSelectResult)packet.GetByte(),
        packet.GetString(),
        packet.GetString())
    { }

    public void Write(Message packet) => packet
        .AddByte((byte)Result)
        .AddString(Token)
        .AddString(HostAddress);
}
