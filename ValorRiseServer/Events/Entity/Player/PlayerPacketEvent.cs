using ValorRise.Packets;
using ValorRiseServer.Entities;

namespace ValorRiseServer;

public class PlayerPacketEvent : IPlayerEvent
{
    private IClientPacket _clientPacket;

    public Player Player { get; }
    public Entity Entity => Player;

    public PlayerPacketEvent(Player player, IClientPacket packet)
    {
        Player = player;
        _clientPacket = packet;
    }

    public IClientPacket GetPacket()
    {
        return _clientPacket;
    }
}