namespace MMO_Library.Server;
using Riptide;

public class Server
{
    private readonly Riptide.Server _server;
    private readonly EntityManager _entityManager;
    private readonly MessageDispatcher _dispatcher;
    private readonly EventBus _eventBus;

    public event EventHandler<ServerConnectedEventArgs> ClientConnected;
    public event EventHandler<ServerDisconnectedEventArgs> ClientDisconnected;

    public EventBus EventBus { get => _eventBus; }
    public EntityManager EntityManager { get => _entityManager; }

    public Server()
    {
        _server = new Riptide.Server();
        _eventBus = new EventBus();
        _dispatcher = new MessageDispatcher(_eventBus, this);
        _entityManager = new EntityManager();

        _server.MessageReceived += (s, e) => _dispatcher.Dispatch(e.FromConnection.Id, e.Message, e.MessageId);
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

    public bool TryGetClient(ushort clientId, out Connection client)
    {
        return _server.TryGetClient(clientId, out client);
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