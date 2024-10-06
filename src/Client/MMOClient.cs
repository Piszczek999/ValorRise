using Riptide;
using Riptide.Utils;

namespace ValorRise.Client;

public class MMOClient
{
    private static MMOClient _instance;
    private Riptide.Client _client;
    private GlobalEventHandler _globalEventHandler;
    private MessageDispatcher _dispatcher;

    public event EventHandler Connected;
    public event EventHandler<DisconnectedEventArgs> Disonnected;
    public event EventHandler<ConnectionFailedEventArgs> ConnectionFailed;

    public static MMOClient Client => _instance;
    public static GlobalEventHandler GlobalEventHandler => _instance._globalEventHandler;

    private MMOClient() { }

    private void Initialize()
    {
        RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.Error.WriteLine, true);
        _client = new Riptide.Client();
        _globalEventHandler = new GlobalEventHandler();
        _dispatcher = new MessageDispatcher();

        _client.MessageReceived += (s, e) => _dispatcher.Dispatch(e.Message, e.MessageId);
        _client.Connected += (s, e) => Connected?.Invoke(this, e);
        _client.Disconnected += (s, e) => Disonnected?.Invoke(this, e);
        _client.ConnectionFailed += (s, e) => ConnectionFailed?.Invoke(this, e);
    }

    public static MMOClient Init()
    {
        _instance ??= new MMOClient();
        _instance.Initialize();
        return _instance;
    }

    public void Connect(string address, ushort port) => _client.Connect($"{address}:{port}", useMessageHandlers: false);

    public void Disconnect() => _client.Disconnect();

    public void Update() => _client.Update();

    public static void Send(Message message) => _instance._client.Send(message);
}