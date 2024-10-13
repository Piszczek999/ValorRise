using System.Numerics;
using ValorRiseServer.Entities;

namespace ValorRiseServer;

public class PlayerMoveClickEvent : IPlayerEvent, ICancellableEvent
{
    public Player Player { get; }
    public Entity Entity => Player;
    public Vector2 ClickPosition { get; set; }

    public bool IsCancelled { get; set; }

    public PlayerMoveClickEvent(Player player, Vector2 clickPosition)
    {
        Player = player;
        ClickPosition = clickPosition;
    }
}