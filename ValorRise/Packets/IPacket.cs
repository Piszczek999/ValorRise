using Riptide;

namespace ValorRise.Packets;

public interface IPacket
{
    void Write(Message message);
}