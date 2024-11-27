using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.RemoveItem, MessageSendMode.Reliable)]
public record RemoveItemPacket(
    ItemType ItemType,
    uint Amount) : IServerPacket
{
    public RemoveItemPacket(Message buffer) : this(
        (ItemType)buffer.GetByte(),
        buffer.GetUInt())
    { }

    public void Write(Message buffer) => buffer
        .AddByte((byte)ItemType)
        .AddUInt(Amount);
}