using MongoDB.Bson;
using ValorRise.Enums;
using ValorRise.Models;
using ValorRise.Packets;
using ValorRise.Packets.Loading.Server;

namespace ValorRiseGameServer.Entities;

public class Player : LivingEntity
{
    public ObjectId UserId { get; set; }
    public ushort MapId { get; set; }
    public ushort Level { get; set; }
    public float Exp { get; set; }
    public ulong Gold { get; set; }
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

    public void SendInfoPackets()
    {
        SendPacket(new MapInfoPacket(MapId));
        SendPacket(new PlayerInfoPacket(
            Id: Id,
            Position: Position,
            Name: Name,
            Level: Level,
            Exp: Exp,
            Health: Health,
            MaxHealth: MaxHealth,
            Mana: Mana,
            MaxMana: MaxMana,
            Gold: Gold
        ));
    }

    public static Player FromCharacter(Character character, PlayerConnection connection)
    {
        return new Player(connection, character.Id)
        {
            UserId = character.UserId,
            Name = character.Name,
            Level = character.Level,
            Exp = character.Exp,
            Gold = character.Gold,
            Health = character.Health,
            MaxHealth = character.MaxHealth,
            Mana = character.Mana,
            MaxMana = character.MaxMana,
            Position = character.Position,
            MapId = character.MapId,
        };
    }
}