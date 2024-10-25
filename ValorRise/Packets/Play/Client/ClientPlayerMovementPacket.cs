using System.Numerics;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Client;

[Packet(PacketType.PlayerMovement, MessageSendMode.Reliable)]
public record ClientPlayerMovementPacket(long Timestamp, Vector2 Destination) : IClientPacket
{
    public ClientPlayerMovementPacket(Vector2 Destination) : this(
        DateTimeOffset.Now.ToUnixTimeMilliseconds(),
        Destination)
    { }

    public ClientPlayerMovementPacket(Message packet) : this(
        Timestamp: packet.GetLong(),
        Destination: packet.GetVector2())
    { }

    public void Write(Message packet) => packet
        .AddLong(Timestamp)
        .AddVector2(Destination);
}