using ValorRiseGameServer.Entities;

namespace ValorRiseGameServer.Events;

public class PlayerJoinEvent : IPlayerEvent
{
    public Player Player { get; }
    public Entity Entity => Player;

    public PlayerJoinEvent(Player player)
    {
        Player = player;
    }
}