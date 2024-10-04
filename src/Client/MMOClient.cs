namespace MMOLibrary.Client;
using Riptide;
using Riptide.Utils;

public class MMOClient
{
    private static MMOClient _instance;
    private readonly Client _client;
    private readonly EventBus _eventBus;
    private readonly MessageDispatcher _dispatcher;

    public event EventHandler Connected;
    public event EventHandler<DisconnectedEventArgs> Disonnected;
    public event EventHandler<ConnectionFailedEventArgs> ConnectionFailed;

    public static EventBus EventBus => _instance._eventBus;

    private MMOClient(ClientType type)
    {
        RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.Error.WriteLine, true);
        _client = new Client();
        _eventBus = new EventBus();
        _dispatcher = new MessageDispatcher(_eventBus, type);

        _client.MessageReceived += (s, e) => _dispatcher.Dispatch(e.Message, e.MessageId);
        _client.Connected += (s, e) => Connected?.Invoke(this, e);
        _client.Disconnected += (s, e) => Disonnected?.Invoke(this, e);
        _client.ConnectionFailed += (s, e) => ConnectionFailed?.Invoke(this, e);
    }

    public static MMOClient Init(ClientType type)
    {
        return _instance ??= new MMOClient(type);
    }

    public void Connect(string address, ushort port)
    {
        _client.Connect($"{address}:{port}", useMessageHandlers: false);
    }

    public void Connect(string address)
    {
        _client.Connect(address, useMessageHandlers: false);
    }

    public void Disconnect()
    {
        _client.Disconnect();
    }

    public void Update()
    {
        _client.Update();
    }

    public static void Send(Message message)
    {
        _instance._client.Send(message);
    }
}