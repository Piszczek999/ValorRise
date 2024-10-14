using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Loading.Server;

[Packet(PacketType.VerifyTokenResponse, MessageSendMode.Reliable)]
public record VerifyTokenResponsePacket(bool Result) : IServerPacket
{
    public VerifyTokenResponsePacket(Message packet) : this(packet.GetBool())
    { }

    public void Write(Message packet) => packet
        .AddBool(Result);
}