using ValorRise.Packets;
using ValorRiseGameServer.Entities;

namespace ValorRiseGameServer.Events;

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