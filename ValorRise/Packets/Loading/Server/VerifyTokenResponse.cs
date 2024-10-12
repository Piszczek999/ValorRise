using System.Numerics;
using MongoDB.Bson;
using Riptide;

namespace ValorRise.Packets.Loading.Server;

[Packet(PacketType.VerifyTokenResponse, MessageSendMode.Reliable)]
public record VerifyTokenResponse(bool Result) : IServerPacket
{
    public VerifyTokenResponse(Message packet) : this(packet.GetBool())
    { }

    public void Write(Message packet) => packet
        .AddBool(Result);
}