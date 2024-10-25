using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Riptide;
using Riptide.Utils;
using ValorRise;
using ValorRise.Packets;

namespace ValorRiseClient;

public class ValorClient
{
    private static ValorClient _instance;
    private readonly Client _client;
    private readonly IServerPacketProcessor _packetProcessor;

    public static ValorClient Client => _instance;
    public static short SmoothRTT => _instance._client.SmoothRTT;
    public static short RTT => _instance._client.RTT;

    public static event EventHandler Connected;
    public static event EventHandler<ConnectionFailedEventArgs> ConnectionFailed;
    public static event EventHandler<DisconnectedEventArgs> Disconnected;

    public ValorClient(IServerPacketProcessor packetProcessor)
    {
        RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.Error.WriteLine, true);
        _packetProcessor = packetProcessor;
        _client = new Client();

        _client.MessageReceived += (s, e) => _packetProcessor.Process(e.MessageId, e.Message);
        _client.Connected += (s, e) => Connected?.Invoke(s, e);
        _client.Disconnected += (s, e) => Disconnected?.Invoke(s, e);
        _client.ConnectionFailed += (s, e) => ConnectionFailed?.Invoke(s, e);
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

    public static void Connect(string hostAddress)
    {
        _instance._client.Connect(hostAddress, useMessageHandlers: false);
    }

    public static void Disconnect()
    {
        _instance._client.Disconnect();
    }

    public static void Update()
    {
        _instance._client.Update();
    }

    public static void SendPacket(IClientPacket packet)
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
        _instance._client.Send(message);
    }
}
