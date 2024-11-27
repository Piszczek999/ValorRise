using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.ObjectDespawn, MessageSendMode.Reliable)]
public record ObjectDespawnPacket(uint Id) : IServerPacket
{
    public ObjectDespawnPacket(Message buffer) : this(buffer.GetUInt()) { }

    public void Write(Message buffer) => buffer
        .AddUInt(Id);
}