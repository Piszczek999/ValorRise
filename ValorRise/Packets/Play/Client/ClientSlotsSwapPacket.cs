using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Client;

[Packet(PacketType.ClientSlotsSwap, MessageSendMode.Reliable)]
public record ClientSlotsSwapPacket(byte FromSlotId, byte ToSlotId) : IClientPacket
{
    public ClientSlotsSwapPacket(Message packet) : this(
        FromSlotId: packet.GetByte(),
        ToSlotId: packet.GetByte())
    { }

    public void Write(Message packet) => packet
        .AddByte(FromSlotId)
        .AddByte(ToSlotId);
}