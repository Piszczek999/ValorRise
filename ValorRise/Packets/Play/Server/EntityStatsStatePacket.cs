using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.EntityStatsState, MessageSendMode.Reliable)]
public record EntityStatsStatePacket(
    uint Id,
    float Health,
    float MaxHealth,
    float AttackSpeed,
    float MoveSpeed,
    float AttackDamage,
    float AbilityPower,
    float Armor) : IServerPacket
{
    public EntityStatsStatePacket(Message buffer) : this(
        buffer.GetUInt(),
        buffer.GetShort(),
        buffer.GetShort(),
        buffer.GetShort() / 100f,
        buffer.GetShort(),
        buffer.GetShort(),
        buffer.GetShort(),
        buffer.GetShort())
    { }

    public void Write(Message buffer) => buffer
        .AddUInt(Id)
        .AddShort((short)(Health + 0.5f))
        .AddShort((short)(MaxHealth + 0.5f))
        .AddShort((short)(AttackSpeed * 100))
        .AddShort((short)(MoveSpeed + 0.5f))
        .AddShort((short)(AttackDamage + 0.5f))
        .AddShort((short)(AbilityPower + 0.5f))
        .AddShort((short)(Armor + 0.5f));
}