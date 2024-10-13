using MongoDB.Bson;
using ValorRise.Enums;
using ValorRise.Packets;

namespace ValorRiseGameServer.Entities;

public class Player : LivingEntity
{
    public ObjectId UserId { get; set; }
    public ushort MapId { get; set; }
    public int Level { get; set; }
    public float Exp { get; set; }
    public long Gold { get; set; }
    public float Mana { get; set; }
    public float MaxMana { get; set; }
    public bool IsOnline { get; set; }

    private PlayerConnection _connection;

    public Player(PlayerConnection connection, ObjectId id) : base(EntityType.Player, id)
    {
        _connection = connection;
        Destination = Position;
        IsCollidable = true;
    }

    public void Disconnect()
    {
        _connection.Disconnect();
    }

    public void SendPacket(IServerPacket packet)
    {
        _connection.SendPacket(packet);
    }
}