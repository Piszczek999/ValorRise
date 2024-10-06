using Riptide;

namespace ValorRise.Server.Entities;

public class Player : LivingEntity
{
    private Connection _connection;

    public Player(Connection connection) : base()
    {
        EntityType = EntityType.Player;
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

    public override void Serialize(Message message)
    {
        base.Serialize(message);
    }

    public override void Deserialize(Message message)
    {
        base.Deserialize(message);
    }
}