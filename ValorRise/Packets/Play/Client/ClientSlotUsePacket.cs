using System.Numerics;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Client;

[Packet(PacketType.ClientSlotUse, MessageSendMode.Reliable)]
public record ClientSlotUsePacket(byte SlotId, Vector2 TargetPosition, uint TargetId) : IClientPacket
{
    public ClientSlotUsePacket(Message packet) : this(
        SlotId: packet.GetByte(),
        TargetPosition: packet.GetVector2(),
        TargetId: packet.GetUInt())
    { }

    public void Write(Message packet) => packet
        .AddByte(SlotId)
        .AddVector2(TargetPosition)
        .AddUInt(TargetId);
}