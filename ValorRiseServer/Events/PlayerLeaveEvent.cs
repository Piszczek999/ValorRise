using ValorRiseServer.Entities;

namespace ValorRiseServer;

public class PlayerLeaveEvent : IPlayerEvent
{
    public Player Player { get; }
    public Entity Entity => Player;

    public PlayerLeaveEvent(Player player)
    {
        Player = player;
    }
}