using Riptide;

public static class ConnectionExtenstion
{
    public static void Disconnect(this Connection connection)
    {
        connection.TimeoutTime = 0;
    }
}