using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.AddItem, MessageSendMode.Reliable)]
public record AddItemPacket(
    ItemType ItemType,
    uint Amount) : IServerPacket
{
    public AddItemPacket(Message buffer) : this(
        (ItemType)buffer.GetByte(),
        buffer.GetUInt())
    { }

    public void Write(Message buffer) => buffer
        .AddByte((byte)ItemType)
        .AddUInt(Amount);
}