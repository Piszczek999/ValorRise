namespace MMO_Library.Server;
using Riptide;

public class Server
{
    private readonly Riptide.Server _server;
    private readonly ConnectionManager _connectionManager;
    private readonly EntityManager _entityManager;
    private readonly MessageDispatcher _dispatcher;
    private readonly EventBus _eventBus;

    public event EventHandler<ServerConnectedEventArgs> ClientConnected;
    public event EventHandler<ServerDisconnectedEventArgs> ClientDisconnected;

    public ConnectionManager ConnectionManager { get => _connectionManager; }
    public EventBus EventBus { get => _eventBus; }
    public EntityManager EntityManager { get => _entityManager; }

    public Server()
    {
        _server = new Riptide.Server();
        _eventBus = new EventBus();
        _connectionManager = new ConnectionManager();
        _dispatcher = new MessageDispatcher(_eventBus, _connectionManager);
        _entityManager = new EntityManager();

        _server.MessageReceived += (s, e) => _dispatcher.Dispatch(e.FromConnection.Id, e.Message, e.MessageId);
        _server.ClientConnected += (s, e) => _connectionManager.AddConnection(e.Client);
        _server.ClientDisconnected += (s, e) => _connectionManager.RemoveConnection(e.Client.Id);
        _server.ClientConnected += (s, e) => ClientConnected?.Invoke(this, e);
        _server.ClientDisconnected += (s, e) => ClientDisconnected?.Invoke(this, e);
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

    public void SendMessage(ushort clientId, Message message)
    {
        _server.Send(message, clientId);
    }

    public void DisconnectPlayer(ushort connectionId)
    {
        _server.DisconnectClient(connectionId);
    }
}