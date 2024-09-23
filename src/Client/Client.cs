namespace MMO_Library.Client;
using Riptide;

public class Client
{
    private readonly Riptide.Client _client;
    private readonly EventBus _eventBus;
    private readonly MessageDispatcher _dispatcher;

    private event EventHandler Connected;
    private event EventHandler<DisconnectedEventArgs> Disonnected;
    private event EventHandler<ConnectionFailedEventArgs> ConnectionFailed;

    public EventBus EventBus { get => _eventBus; }

    public Client()
    {
        _client = new Riptide.Client();
        _eventBus = new EventBus();
        _dispatcher = new MessageDispatcher(_eventBus);

        _client.MessageReceived += (s, e) => _dispatcher.Dispatch(e.Message, e.MessageId);
        _client.Connected += (s, e) => Connected.Invoke(this, e);
        _client.Disconnected += (s, e) => Disonnected.Invoke(this, e);
        _client.ConnectionFailed += (s, e) => ConnectionFailed.Invoke(this, e); ;
    }

    public void Connect(string address, ushort port)
    {
        _client.Connect($"{address}:{port}", useMessageHandlers: false);
    }

    public void Disconnect()
    {
        _client.Disconnect();
    }

    public void Update()
    {
        _client.Update();
    }

    public void SendMessage(Message message)
    {
        _client.Send(message);
    }
}