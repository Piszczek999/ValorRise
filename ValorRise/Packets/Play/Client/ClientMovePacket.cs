using System.Numerics;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Client;

[Packet(PacketType.ClientMove, MessageSendMode.Reliable)]
public record ClientMovePacket(long Timestamp, Vector2 Destination) : IClientPacket
{
    public ClientMovePacket(Vector2 Destination) : this(
        DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
        Destination)
    { }

    public ClientMovePacket(Message packet) : this(
        Timestamp: packet.GetLong(),
        Destination: packet.GetVector2())
    { }

    public void Write(Message packet) => packet
        .AddLong(Timestamp)
        .AddVector2(Destination);
}