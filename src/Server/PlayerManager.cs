using ValorRise.Server.Entities;

namespace ValorRise.Server;

public class PlayerManager
{
    private readonly Dictionary<ushort, Player> _players = new();

    public Player AddPlayer(ushort clientId, Player player)
    {
        _players.Add(clientId, player);
        return player;
    }

    public void RemovePlayer(ushort clientId)
    {
        _players.Remove(clientId);
    }

    public bool TryGetPlayer(ushort clientId, out Player player)
    {
        return _players.TryGetValue(clientId, out player);
    }

    public IEnumerable<Player> GetPlayers()
    {
        return _players.Values;
    }
}