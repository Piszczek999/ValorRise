using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Loading.Server;

[Packet(PacketType.PlayerInfo, MessageSendMode.Reliable)]
public record PlayerInfoPacket(
    uint Id,
    byte Level,
    uint Exp,
    uint Gold,
    float Mana,
    float MaxMana,
    float AttackSpeed) : IServerPacket
{
    public PlayerInfoPacket(Message packet) : this(
        packet.GetUInt(),
        packet.GetByte(),
        packet.GetUInt(),
        packet.GetUInt(),
        packet.GetFloat(),
        packet.GetFloat(),
        packet.GetFloat())
    { }

    public void Write(Message packet) => packet
        .AddUInt(Id)
        .AddByte(Level)
        .AddUInt(Exp)
        .AddUInt(Gold)
        .AddFloat(Mana)
        .AddFloat(MaxMana)
        .AddFloat(AttackSpeed);
}