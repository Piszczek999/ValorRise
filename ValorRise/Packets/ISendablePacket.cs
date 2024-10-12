using Riptide;

namespace ValorRise.Packets;

public interface ISendablePacket
{
    void Write(Message message);
}