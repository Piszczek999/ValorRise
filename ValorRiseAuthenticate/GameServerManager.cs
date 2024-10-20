using ValorRise;
using ValorRiseAuthenticate;

namespace ValorRiseAuthenticate;

public class GameServerInfo
{
    public ClientConnection Connection { get; set; }
    public ushort Port { get; set; }
    public string IpAddress { get; set; }
    public string HostAddress => $"{IpAddress}:{Port}";
    public Map Map { get; set; }
}

public enum Map
{
    City,
}

public class GameServerManager
{
    private const ushort MIN_PORT = 1303;
    private const ushort MAX_SERVERS_PER_IP = 50;
    private readonly Queue<Map> _freeMaps = new();
    private readonly Dictionary<ushort, GameServerInfo> _gameServers = new();

    public GameServerManager()
    {
        foreach (var map in Enum.GetValues<Map>())
        {
            _freeMaps.Enqueue(map);
        }
    }

    public bool TryAddServer(ClientConnection connection, string ipAddress, out GameServerInfo serverInfo)
    {
        serverInfo = default;
        // Check if the game server for this connection already exists
        if (_gameServers.ContainsKey(connection.Id))
        {
            return false;
        }

        // Find the lowest unused port in the range starting from MIN_PORT
        for (int i = 0; i < MAX_SERVERS_PER_IP; i++)
        {
            var port = (ushort)(MIN_PORT + i);
            if (!_gameServers.Values.Any(server => server.IpAddress == ipAddress && server.Port == port)
            && _freeMaps.TryDequeue(out var map))
            {
                serverInfo = new GameServerInfo
                {
                    Connection = connection,
                    IpAddress = ipAddress,
                    Port = port,
                    Map = map
                };
                _gameServers[connection.Id] = serverInfo;
                return true;
            }
        }
        return false;
    }

    public bool TryGetServerByMap(Map map, out GameServerInfo gameServer)
    {
        gameServer = _gameServers.Values.FirstOrDefault(server => server.Map == map);
        return gameServer != null;
    }

    public bool TryGetServer(ushort id, out GameServerInfo gameServer)
    {
        return _gameServers.TryGetValue(id, out gameServer);
    }

    public void RemoveServer(ushort id)
    {
        if (_gameServers.Remove(id, out var serverInfo))
        {
            _freeMaps.Enqueue(serverInfo.Map);
        }
    }
}