using Riptide;

namespace MMOLibrary.Server;

public class Player : LivingEntity
{
    private Connection _connection;

    public Player(Connection connection) : base()
    {
        _connection = connection;
    }

    public void Disconnect()
    {
        _connection.Disconnect();
    }

    public void Send(Message message)
    {
        _connection.Send(message);
    }
}