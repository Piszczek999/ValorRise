using Riptide;
using ValorRise.Enums;
using ValorRise.Models;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.InventoryUpdate, MessageSendMode.Reliable)]
public record InventoryUpdatePacket(ItemDB Item) : IServerPacket
{
    public InventoryUpdatePacket(Message buffer) : this(
        buffer.GetSerializable<ItemDB>())
    { }

    public void Write(Message buffer) => buffer
        .AddSerializable(Item);
}