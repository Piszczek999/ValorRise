namespace MMO_Library.Server;

using Riptide;

public class Connection
{
    private Riptide.Connection _connection;

    public Connection(Riptide.Connection connection)
    {
        _connection = connection;
    }

    public void Send(Message message)
    {
        _connection.Send(message);
    }

    public void Disconnect()
    {
        _connection.TimeoutTime = 0;
    }
}