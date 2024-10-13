using System.Reflection;
using Riptide;
using ValorRise.Packets;
using ValorRiseGameServer.Entities;

namespace ValorRiseGameServer;

public class PlayerConnection
{
    private readonly Connection _connection;
    public Player Player { get; set; }

    public PlayerConnection(Connection connection)
    {
        _connection = connection;
    }

    public void SendPacket(IServerPacket packet)
    {
        var attribute = packet.GetType().GetCustomAttribute<PacketAttribute>();
        if (attribute == null)
        {
            throw new InvalidOperationException($"Packet type {packet.GetType().Name} does not have a PacketAttribute.");
        }

        var packetId = attribute.PacketId;
        var sendMode = attribute.SendMode;

        var message = Message.Create(sendMode, packetId);
        packet.Write(message);
        _connection.Send(message);
    }

    public void Disconnect()
    {
        _connection.TimeoutTime = 0;
    }
}