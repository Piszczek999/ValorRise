using MongoDB.Bson;
using Riptide;

public static class ConnectionExtenstion
{
    private static readonly Dictionary<Connection, ObjectId> connectionUserMap = new Dictionary<Connection, ObjectId>();

    public static void SetUserId(this Connection connection, ObjectId userId)
    {
        connectionUserMap[connection] = userId;
    }

    public static ObjectId GetUserId(this Connection connection)
    {
        return connectionUserMap.TryGetValue(connection, out var userId) ? userId : ObjectId.Empty;
    }
    public static void Disconnect(this Connection connection)
    {
        connection.TimeoutTime = 0;
    }
}