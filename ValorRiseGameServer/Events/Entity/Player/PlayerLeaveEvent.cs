using ValorRiseGameServer.Entities;

namespace ValorRiseGameServer.Events;

public class PlayerLeaveEvent : IPlayerEvent
{
    public Player Player { get; }
    public Entity Entity => Player;

    public PlayerLeaveEvent(Player player)
    {
        Player = player;
    }
}