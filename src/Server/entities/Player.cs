namespace ValorRiseServer.Entities;
using Riptide;
using ValorRise.Entities;

public class Player : LivingEntity, IPlayer
{
    private Connection _connection;

    public Player(Connection connection) : base()
    {
        _connection = connection;
    }

    public void Disconnect()
    {
        _connection.TimeoutTime = 0;
    }

    public void Send(Message message)
    {
        _connection.Send(message);
    }

    public override void Serialize(Message message)
    {
        base.Serialize(message);

    }
}