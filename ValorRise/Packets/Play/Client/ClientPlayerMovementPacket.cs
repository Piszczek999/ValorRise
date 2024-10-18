using System.Numerics;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Client;

[Packet(PacketType.PlayerMovement, MessageSendMode.Reliable)]
public record ClientPlayerMovementPacket(Vector2 Destination) : IClientPacket
{
    public ClientPlayerMovementPacket(Message packet) : this(
        Destination: packet.GetVector2())
    { }

    public void Write(Message packet) => packet
        .AddVector2(Destination);
}