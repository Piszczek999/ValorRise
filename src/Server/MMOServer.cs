using Riptide;
using Riptide.Utils;

namespace ValorRise.Server;

public class MMOServer
{
    private static MMOServer _instance;
    private readonly Riptide.Server _server;
    private readonly EntityManager _entityManager;
    private readonly MessageDispatcher _dispatcher;
    private readonly EventBus _eventBus;

    public event EventHandler<ServerConnectedEventArgs> ClientConnected;
    public event EventHandler<ServerDisconnectedEventArgs> ClientDisconnected;

    public static EventBus EventBus => _instance._eventBus;
    public static EntityManager EntityManager => _instance._entityManager;

    private MMOServer()
    {
        RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.Error.WriteLine, true);
        _server = new Riptide.Server();
        _eventBus = new EventBus();
        _dispatcher = new MessageDispatcher(_eventBus);
        _entityManager = new EntityManager();

        _server.MessageReceived += (s, e) => _dispatcher.Dispatch(e.FromConnection.Id, e.Message, e.MessageId);
        _server.ClientConnected += (s, e) => ClientConnected?.Invoke(this, e);
        _server.ClientDisconnected += (s, e) => ClientDisconnected?.Invoke(this, e);
    }

    public static MMOServer Init()
    {
        return _instance ??= new MMOServer();
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

    public static bool TryGetClient(ushort clientId, out Connection client)
    {
        return _instance._server.TryGetClient(clientId, out client);
    }

    public static void SendMessage(ushort clientId, Message message)
    {
        _instance._server.Send(message, clientId);
    }

    public void DisconnectPlayer(ushort connectionId)
    {
        _server.DisconnectClient(connectionId);
    }
}