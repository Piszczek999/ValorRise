using System.Numerics;
using Riptide;

namespace ValorRise.Packets.Play.Client;

[Packet(PacketType.PlayerMovement, MessageSendMode.Reliable)]
public record ClientPlayerMovementPacket(Vector2 Destination) : IClientPacket
{
    public ClientPlayerMovementPacket(Message packet) : this(
        packet.GetVector2())
    { }

    public void Write(Message packet) => packet
        .AddVector2(Destination);
}