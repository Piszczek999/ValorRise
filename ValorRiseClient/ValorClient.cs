using Microsoft.Extensions.DependencyInjection;
using Riptide;
using Riptide.Utils;

namespace ValorRiseClient;

public class ValorClient
{
    private static ValorClient _instance;
    private readonly Client _client;
    private readonly IPacketProcessor _packetProcessor;
    private readonly IPacketListenerManager _listenerManager;

    public static ValorClient Server => _instance;
    public static IPacketListenerManager ListenerManager => _instance._listenerManager;

    public event EventHandler Connected;
    public event EventHandler<ConnectionFailedEventArgs> ConnectionFailed;
    public event EventHandler<DisconnectedEventArgs> Disconnected;

    private ValorClient(IPacketProcessor packetProcessor, IPacketListenerManager packetListenerManager)
    {
        RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.Error.WriteLine, true);
        _packetProcessor = packetProcessor;
        _listenerManager = packetListenerManager;
        _client = new Client();

        _client.MessageReceived += (s, e) => _packetProcessor.Process(e.MessageId, e.Message);
        _client.Connected += Connected;
        _client.Disconnected += Disconnected;
        _client.ConnectionFailed += ConnectionFailed;
    }

    public static ValorClient Init()
    {
        if (_instance != null) return _instance;

        var serviceProvider = new ServiceCollection()
            .AddSingleton<IPacketProcessor, PacketProcessor>()
            .AddSingleton<IPacketListenerManager, PacketListenerManager>()
            .AddSingleton<ValorClient>()
            .BuildServiceProvider();

        return _instance = serviceProvider.GetService<ValorClient>();
    }

    public void Connect(string ipAddress, ushort port)
    {
        _client.Connect($"{ipAddress}:{port}", useMessageHandlers: false);
    }

    public void Disconnect()
    {
        _client.Disconnect();
    }

    public void Update()
    {
        _client.Update();
    }
}
