using System.Numerics;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Client;

[Packet(PacketType.Move, MessageSendMode.Reliable)]
public record MovePacket(long Timestamp, Vector2 Destination) : IClientPacket
{
    public MovePacket(Vector2 Destination) : this(
        DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
        Destination)
    { }

    public MovePacket(Message packet) : this(
        Timestamp: packet.GetLong(),
        Destination: packet.GetVector2())
    { }

    public void Write(Message packet) => packet
        .AddLong(Timestamp)
        .AddVector2(Destination);
}