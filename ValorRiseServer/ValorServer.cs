using Microsoft.Extensions.DependencyInjection;
using Riptide;
using Riptide.Utils;

namespace ValorRiseServer;

public class ValorServer
{
    private static ValorServer _instance;
    private readonly Server _server;
    private readonly IClientPacketProcessor _packetProcessor;
    private readonly IClientPacketListenerManager _packetListenerManager;
    private readonly Dictionary<ushort, ClientConnection> _connections = new();

    public static ValorServer Server => _instance;
    public static IClientPacketListenerManager PacketListenerManager => _instance._packetListenerManager;
    public event EventHandler<ServerConnectedEventArgs> ClientConnected;
    public event EventHandler<ServerDisconnectedEventArgs> ClientDisconnected;

    private ValorServer(IClientPacketProcessor packetProcessor, IClientPacketListenerManager packetListenerManager)
    {
        RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.Error.WriteLine, true);
        _packetProcessor = packetProcessor;
        _packetListenerManager = packetListenerManager;
        _server = new Server();

        _server.MessageReceived += (s, e) => _packetProcessor.Process(_connections[e.FromConnection.Id], e.MessageId, e.Message);
        _server.ClientConnected += ClientConnected;
        _server.ClientConnected += (s, e) => _connections.TryAdd(e.Client.Id, new ClientConnection(e.Client));
        _server.ClientDisconnected += ClientDisconnected;
        _server.ClientDisconnected += (s, e) => _connections.Remove(e.Client.Id);
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

    public void Start(ushort port, ushort maxClients)
    {
        _server.Start(port, maxClients, useMessageHandlers: false);
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

    public static void SendToAll(Message packet)
    {
        _instance._server.SendToAll(packet);
    }

    public static void SendToAll(Message packet, ushort exceptToClientId)
    {
        _instance._server.SendToAll(packet, exceptToClientId);
    }

    public void DisconnectPlayer(ushort connectionId)
    {
        _server.DisconnectClient(connectionId);
    }
}