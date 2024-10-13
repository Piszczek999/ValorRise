using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Riptide;
using Riptide.Utils;
using ValorRise.Packets;

namespace ValorRiseClient;

public class ValorClient
{
    private static ValorClient _instance;
    private readonly Client _client;
    private readonly IServerPacketProcessor _packetProcessor;
    private readonly IServerPacketListenerManager _listenerManager;

    public static ValorClient Client => _instance;
    public static IServerPacketListenerManager ListenerManager => _instance._listenerManager;

    public event EventHandler Connected;
    public event EventHandler<ConnectionFailedEventArgs> ConnectionFailed;
    public event EventHandler<DisconnectedEventArgs> Disconnected;

    public ValorClient(IServerPacketProcessor packetProcessor, IServerPacketListenerManager packetListenerManager)
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
            .AddSingleton<IServerPacketProcessor, ServerPacketProcessor>()
            .AddSingleton<IServerPacketListenerManager, ServerPacketListenerManager>()
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

    public void SendPacket(IClientPacket packet)
    {
        var attribute = packet.GetType().GetCustomAttribute<PacketAttribute>();
        if (attribute == null)
        {
            throw new InvalidOperationException($"Packet type {packet.GetType().Name} does not have a PacketAttribute.");
        }

        var packetId = attribute.PacketId;
        var sendMode = attribute.SendMode;

        var message = Message.Create(sendMode, packetId);
        packet.Write(message);
        _client.Send(message);
    }
}
