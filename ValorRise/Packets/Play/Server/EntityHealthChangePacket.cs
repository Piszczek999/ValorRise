using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.EntityHealthChange, MessageSendMode.Reliable)]
public record EntityHealthChangePacket(
    uint Id,
    float Health,
    float MaxHealth) : IServerPacket
{
    public EntityHealthChangePacket(Message buffer) : this(
        buffer.GetUInt(),
        buffer.GetFloat(),
        buffer.GetFloat())
    { }

    public void Write(Message buffer) => buffer
        .AddUInt(Id)
        .AddFloat(Health)
        .AddFloat(MaxHealth);
}