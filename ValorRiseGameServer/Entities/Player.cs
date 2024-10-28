using System.Numerics;
using MongoDB.Bson;
using ValorRise.Enums;
using ValorRise.Models;
using ValorRise.Packets;
using ValorRise.Packets.Loading.Server;
using ValorRise.Packets.Play.Server;

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

    public Player(PlayerConnection connection, ObjectId id, string name, Vector2 position) : base(EntityType.Player, id, name, position)
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

    public void SendSpawnInfoPackets()
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

        foreach (var entity in ValorServer.EntityManager.GetEntities().Where(e => e.Id != Id))
        {
            SendPacket(new SpawnEntityPacket(entity.Id, entity.EntityType, entity.Position, entity.Position));
        }
    }

    public static Player FromCharacter(Character character, PlayerConnection connection)
    {
        return new Player(connection, character.Id, character.Name, character.Position)
        {
            UserId = character.UserId,
            Level = character.Level,
            Exp = character.Exp,
            Gold = character.Gold,
            Health = character.Health,
            MaxHealth = character.MaxHealth,
            Mana = character.Mana,
            MaxMana = character.MaxMana,
            MapId = character.MapId,
            Speed = 100f
        };
    }

    public Character ToCharacter()
    {
        return new Character()
        {
            Id = Id,
            UserId = UserId,
            Name = Name,
            Level = Level,
            Exp = Exp,
            Gold = Gold,
            Health = Health,
            MaxHealth = MaxHealth,
            Mana = Mana,
            MaxMana = MaxMana,
            Position = Position,
            MapId = MapId,
        };
    }
}