namespace MMO_Library.Server;

using MongoDB.Bson;
using Riptide;

public interface IConnection
{
    void Send(Message message);
    void Disconnect();
}

public class Connection : IConnection
{
    private Riptide.Connection _connection;
    public ObjectId UserId { get; set; }

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
        _connection.Disconnect();
    }
}