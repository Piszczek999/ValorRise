using System.Reflection;
using MongoDB.Bson;
using Riptide;
using ValorRise.Packets;

namespace ValorRiseGateway;

public class ClientConnection
{
    private readonly Connection _connection;
    public ObjectId UserId { get; set; }
    public ushort Id => _connection.Id;

    public ClientConnection(Connection connection)
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
