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

    public event EventHandler Connected;
    public event EventHandler<ConnectionFailedEventArgs> ConnectionFailed;
    public event EventHandler<DisconnectedEventArgs> Disconnected;

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

    public void Connect(string hostAddress)
    {
        _client.Connect(hostAddress, useMessageHandlers: false);
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
