using Riptide;
using ValorRise.Enums;
using ValorRise.Models;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.SlotUpdate, MessageSendMode.Reliable)]
public record SlotUpdatePacket(SlotDB Item) : IServerPacket
{
    public SlotUpdatePacket(Message buffer) : this(
        buffer.GetSerializable<SlotDB>())
    { }

    public void Write(Message buffer) => buffer
        .AddSerializable(Item);
}