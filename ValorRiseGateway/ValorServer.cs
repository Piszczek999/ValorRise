using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using Riptide;
using Riptide.Utils;
using ValorRise.Packets.Authentication.GameServer;

namespace ValorRiseGateway;

public class ValorServer
{
    private static ValorServer _instance;
    private readonly Server _server;
    private readonly IClientPacketProcessor _packetProcessor;
    private readonly IClientPacketListenerManager _packetListenerManager;
    private readonly Dictionary<ushort, ClientConnection> _connections = new();

    public static ValorServer Server => _instance;
    public static IClientPacketListenerManager PacketListenerManager => _instance._packetListenerManager;

    public ValorServer(IClientPacketProcessor packetProcessor, IClientPacketListenerManager packetListenerManager)
    {
        RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.Error.WriteLine, true);
        _packetProcessor = packetProcessor;
        _packetListenerManager = packetListenerManager;
        _server = new Server();

        _server.MessageReceived += (s, e) => _packetProcessor.Process(_connections[e.FromConnection.Id], e.MessageId, e.Message);
        _server.ClientConnected += ClientConnected;
        _server.ClientDisconnected += ClientDisconnected;

        _server.Start(1301, 500, useMessageHandlers: false);
    }

    public static ValorServer Init()
    {
        if (_instance != null) return _instance;

        var serviceProvider = new ServiceCollection()
            .AddSingleton<IClientPacketProcessor, ClientPacketProcessor>()
            .AddSingleton<IClientPacketListenerManager, ClientPacketListenerManager>()
            .AddSingleton<ValorServer>()
            .BuildServiceProvider();

        return _instance = serviceProvider.GetService<ValorServer>();
    }

    private void ClientConnected(object sender, ServerConnectedEventArgs e)
    {
        _connections.TryAdd(e.Client.Id, new ClientConnection(e.Client));
    }

    private void ClientDisconnected(object sender, ServerDisconnectedEventArgs e)
    {
        if (_connections.TryGetValue(e.Client.Id, out var connection) && connection.UserId != ObjectId.Empty)
        {
            ValorClient.Client.SendPacket(new UserLogoutPacket(connection.UserId));
        }
        _connections.Remove(e.Client.Id);
    }

    public void Stop()
    {
        _server.Stop();
    }

    public void Update()
    {
        _server.Update();
    }

    public static bool TryGetClient(ushort clientId, out ClientConnection client)
    {
        return _instance._connections.TryGetValue(clientId, out client);
    }

    public void SendToAll(Message packet)
    {
        _server.SendToAll(packet);
    }

    public void SendToAll(Message packet, ushort exceptToClientId)
    {
        _server.SendToAll(packet, exceptToClientId);
    }

    public void DisconnectPlayer(ushort connectionId)
    {
        _server.DisconnectClient(connectionId);
    }
}