using Microsoft.Extensions.DependencyInjection;
using Riptide;
using Riptide.Utils;

namespace ValorRiseAuthenticate;

public class ValorServer
{
    private static ValorServer _instance;
    private readonly Server _server;
    private readonly IClientPacketProcessor _packetProcessor;
    private readonly GameServerManager _gameServerManager;
    private readonly Dictionary<ushort, ClientConnection> _connections = new();

    public static ValorServer Server => _instance;
    public static GameServerManager GameServerManager => _instance._gameServerManager;

    public ValorServer(IClientPacketProcessor packetProcessor, GameServerManager gameServerManager)
    {
        RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.Error.WriteLine, true);
        _packetProcessor = packetProcessor;
        _gameServerManager = gameServerManager;
        _server = new Server();

        _server.MessageReceived += (s, e) => _packetProcessor.Process(_connections[e.FromConnection.Id], e.MessageId, e.Message);
        _server.ClientConnected += ClientConnected;
        _server.ClientDisconnected += ClientDisconnected;

        _server.Start(1302, 5, useMessageHandlers: false);
    }

    public static ValorServer Init()
    {
        if (_instance != null) return _instance;

        var serviceProvider = new ServiceCollection()
            .AddSingleton<IClientPacketProcessor, ClientPacketProcessor>()
            .AddSingleton<IClientPacketListenerManager, ClientPacketListenerManager>()
            .AddSingleton<GameServerManager>()
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
        _gameServerManager.RemoveServer(e.Client.Id);
        _connections.Remove(e.Client.Id);
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