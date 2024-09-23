namespace MMO_Library.Server;

public class ConnectionManager
{
    private readonly Dictionary<ushort, Connection> _connections = new();

    public void AddConnection(ushort connectionId, Connection connection)
    {
        _connections[connectionId] = connection;
    }

    public void RemoveConnection(ushort connectionId)
    {
        _connections.Remove(connectionId);
    }

    public Connection GetConnection(ushort connectionId)
    {
        _connections.TryGetValue(connectionId, out var userId);
        return userId;
    }
}