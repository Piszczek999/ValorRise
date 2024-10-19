using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;
using ValorRise.Models;

namespace ValorRise.Packets.Authentication.GameServer;

[Packet(PacketType.CharacterSwitchServer, MessageSendMode.Reliable)]
public record CharacterSwitchServerPacket(string Token, Character Character) : IClientPacket
{
    public CharacterSwitchServerPacket(Message packet) : this(
        packet.GetString(),
        packet.GetSerializable<Character>())
    {

    }

    public void Write(Message packet) => packet
        .AddString(Token)
        .AddSerializable(Character);
}
