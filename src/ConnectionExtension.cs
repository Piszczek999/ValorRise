using MongoDB.Bson;
using Riptide;

public static class ConnectionExtension
{
    public static void Disconnect(this Connection connection)
    {
        connection.TimeoutTime = 0;
    }
}