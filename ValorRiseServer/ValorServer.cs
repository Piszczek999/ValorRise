using Microsoft.Extensions.DependencyInjection;
using Riptide;
using Riptide.Utils;
using ValorRiseServer.Entities;

namespace ValorRiseServer;

public class ValorServer
{
    private static ValorServer _instance;
    private readonly Server _server;
    private readonly Dictionary<ushort, PlayerConnection> _connections = new();
    private readonly IClientPacketProcessor _packetProcessor;
    private readonly IEventNode<IEvent> _globalEventNode;
    private readonly ITokenVerificationManager _verificationManager;
    private readonly IEntityManager _entityManager;

    public static ValorServer Server => _instance;
    public static IEventNode<IEvent> GlobalEventNode => _instance._globalEventNode;
    public static ITokenVerificationManager VerificationManager => _instance._verificationManager;
    public static IEntityManager EntityManager => _instance._entityManager;

    private ValorServer(IClientPacketProcessor packetProcessor, ITokenVerificationManager verificationManager, IEventNode<IEvent> globalEventNode, IEntityManager entityManager)
    {
        RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.Error.WriteLine, true);
        _packetProcessor = packetProcessor;
        _verificationManager = verificationManager;
        _globalEventNode = globalEventNode;
        _entityManager = entityManager;
        _server = new Server();

        _server.MessageReceived += (s, e) => _packetProcessor.Process(_connections[e.FromConnection.Id], e.MessageId, e.Message);
        _server.ClientConnected += ClientConnected;
        _server.ClientDisconnected += ClientDisconnected;
    }

    public static ValorServer Init()
    {
        if (_instance != null) return _instance;

        var serviceProvider = new ServiceCollection()
            .AddSingleton<IEventNode<IEvent>, EventNode<IEvent>>()
            .AddSingleton<IClientPacketProcessor, ClientPacketProcessor>()
            .AddSingleton<IClientPacketListenerManager, ClientPacketListenerManager>()
            .AddSingleton<ITokenVerificationManager, TokenVerificationManager>()
            .AddSingleton<IEntityManager, EntityManager>()
            .AddSingleton<ValorServer>()
            .BuildServiceProvider();

        return _instance = serviceProvider.GetService<ValorServer>();
    }

    private void ClientConnected(object sender, ServerConnectedEventArgs e)
    {
        var playerConnection = new PlayerConnection(e.Client);
        _connections.TryAdd(e.Client.Id, playerConnection);
        _verificationManager.Start(playerConnection);
    }

    private void ClientDisconnected(object sender, ServerDisconnectedEventArgs e)
    {
        _connections.TryGetValue(e.Client.Id, out var connection);
        _globalEventNode.Invoke(new PlayerLeaveEvent(connection.Player));
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

    public void PhysicsUpdate(double delta)
    {
        foreach (IMoveable moveable in _entityManager.GetEntities().OfType<IMoveable>())
        {
            if (moveable.UpdatePosition(delta))
            {
                _globalEventNode.Invoke(new EntityMoveEvent((Entity)moveable));
            }
        }
    }

    public static bool TryGetClient(ushort clientId, out Connection client)
    {
        return _instance._server.TryGetClient(clientId, out client);
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