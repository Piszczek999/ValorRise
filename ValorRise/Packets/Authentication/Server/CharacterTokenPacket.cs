using Riptide;
using ValorRise.Enums;
using ValorRise.Models;

namespace ValorRise.Packets.Authentication.Server;

[Packet(PacketType.CharacterToken, MessageSendMode.Reliable)]
public record CharacterTokenPacket(string Token, Character Character) : IServerPacket
{
    public CharacterTokenPacket(Message packet) : this(packet.GetString(), packet.GetSerializable<Character>())
    { }

    public void Write(Message packet) => packet
        .AddString(Token)
        .AddSerializable(Character);
}
